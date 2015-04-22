// Copyright (c) 2011 Blue Onion Software, All rights reserved
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using BlueOnion.Properties;

namespace BlueOnion
{
    using NM = NativeMethods;

    internal static class Desktop
    {
        private static IntPtr GetDesktopHandle()
        {
            var vHandle = NM.FindWindow("Progman", "Program Manager");
            vHandle = NM.FindWindowEx(vHandle, IntPtr.Zero, "SHELLDLL_DefView", null);

            if (vHandle == IntPtr.Zero)
            {
                NM.EnumWindows((hwnd, lp) =>
                {
                    vHandle = NM.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                    return vHandle == IntPtr.Zero;
                }, IntPtr.Zero);
            }

            return NM.FindWindowEx(vHandle, IntPtr.Zero, "SysListView32", "FolderView");
        }

        private static IntPtr GetDesktopProcess(IntPtr desktopHandle)
        {
            uint vProcessId;
            NM.GetWindowThreadProcessId(desktopHandle, out vProcessId);
            return NM.OpenProcess(NM.PROCESS_VM_OPERATION | NM.PROCESS_VM_READ | NM.PROCESS_VM_WRITE, false, vProcessId);
        }

        private static string GetIconTitle(IntPtr desktopHandle, IntPtr desktopProcess, int index)
        {
            var vPointer = NM.VirtualAllocEx(desktopProcess, IntPtr.Zero, (IntPtr) 2048,
                NM.MEM_RESERVE | NM.MEM_COMMIT, NM.PAGE_READWRITE);

            try
            {
                var vBuffer = new byte[256];
                var vNumberOfBytesRead = new IntPtr(0);

                var vItem = new NativeMethods.LVITEM[1];
                vItem[0].mask = NM.LVIF_TEXT;
                vItem[0].iItem = index;
                vItem[0].iSubItem = 0;
                vItem[0].cchTextMax = vBuffer.Length;
                vItem[0].pszText = (IntPtr) ((int) vPointer + Marshal.SizeOf(typeof(NativeMethods.LVITEM)));

                NM.WriteProcessMemory(desktopProcess, vPointer,
                    Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
                    (IntPtr) Marshal.SizeOf(typeof(NativeMethods.LVITEM)), ref vNumberOfBytesRead);

                if (NM.SendMessage(desktopHandle, NM.LVM_GETITEMW, index, vPointer.ToInt32()) == 0)
                    return string.Empty;

                NM.ReadProcessMemory(desktopProcess,
                    (IntPtr) ((int) vPointer + Marshal.SizeOf(typeof(NativeMethods.LVITEM))),
                    Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0),
                    (IntPtr) vBuffer.Length, ref vNumberOfBytesRead);

                return Encoding.Unicode.GetString(vBuffer, 0, (int) vNumberOfBytesRead);
            }

            finally
            {
                NM.VirtualFreeEx(desktopProcess, vPointer, (IntPtr) 0, NM.MEM_RELEASE);
            }
        }

        public static Point? WaitForShortcut(string name)
        {
            var max = 20;
            Point? location;

            do
            {
                Thread.Sleep(50);
                location = GetIconPosition(name);
            } while (location == null && max-- > 0);

            return location;
        }

        public static Point? GetIconPosition(string title)
        {
            var desktopHandle = GetDesktopHandle();
            var desktopProcess = GetDesktopProcess(desktopHandle);

            var vItemCount = NM.SendMessage(desktopHandle, NM.LVM_GETITEMCOUNT, 0, 0);

            for (var item = 0; item < vItemCount; item++)
            {
                var iconTitle = GetIconTitle(desktopHandle, desktopProcess, item);

                if (string.Compare(title, 0, iconTitle, 0, title.Length, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    var vPointer = NM.VirtualAllocEx(desktopProcess, IntPtr.Zero, (IntPtr) 100,
                        NM.MEM_RESERVE | NM.MEM_COMMIT, NM.PAGE_READWRITE);

                    try
                    {
                        if (NM.SendMessage(desktopHandle, NM.LVM_GETITEMPOSITION, item, vPointer.ToInt32()) == 0)
                            return null;

                        var vPoint = new Point[1];
                        var vNumberOfBytesRead = new IntPtr(0);

                        NM.ReadProcessMemory(desktopProcess, vPointer,
                            Marshal.UnsafeAddrOfPinnedArrayElement(vPoint, 0),
                            (IntPtr) Marshal.SizeOf(typeof(Point)), ref vNumberOfBytesRead);

                        vPoint[0].X += Settings.Default.MonitorOffset.Width;
                        vPoint[0].Y += Settings.Default.MonitorOffset.Height;
                        return vPoint[0];
                    }

                    finally
                    {
                        NM.VirtualFreeEx(desktopProcess, vPointer, (IntPtr) 0, NM.MEM_RELEASE);
                    }
                }
            }

            return null;
        }

        public static void SetIconPosition(string title, Point position)
        {
            if (string.IsNullOrEmpty(title))
                return;

            var desktopHandle = GetDesktopHandle();
            var desktopProcess = GetDesktopProcess(desktopHandle);
            var vItemCount = NM.SendMessage(desktopHandle, NM.LVM_GETITEMCOUNT, 0, 0);

            for (var item = 0; item < vItemCount; item++)
            {
                var iconTitle = GetIconTitle(desktopHandle, desktopProcess, item);

                if (string.Compare(title, 0, iconTitle, 0, title.Length, StringComparison.CurrentCultureIgnoreCase) == 0)
                    NM.SendMessage(desktopHandle, NM.LVM_SETITEMPOSITION, item, (uint) position.X + ((uint) position.Y*0x10000));
            }
        }

        private const string IconPositionsFile = "IconPositions.txt";
        private static Dictionary<string, Point> _desktopIconPositions;

        public static void SaveIconPosition(string title, Point? position)
        {
            if (string.IsNullOrEmpty(title))
                return;

            if (position == null)
                return;

            if (_desktopIconPositions.ContainsKey(title))
                _desktopIconPositions[title] = (Point) position;

            else
                _desktopIconPositions.Add(title, (Point) position);
        }

        public static Point? LoadIconPosition(string title)
        {
            if (_desktopIconPositions.ContainsKey(title))
                return _desktopIconPositions[title];

            return null;
        }

        public static void LoadIconPositions()
        {
            try
            {
                using (var isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
                using (var stream = new IsolatedStorageFileStream(IconPositionsFile, FileMode.Open, isoStore))
                using (var textStream = new StreamReader(stream))
                {
                    string line;
                    _desktopIconPositions = new Dictionary<string, Point>();

                    while ((line = textStream.ReadLine()) != null)
                    {
                        var items = line.Split(null, 3);
                        var point = new Point
                        {
                            X = Convert.ToInt32(items[0], CultureInfo.InvariantCulture),
                            Y = Convert.ToInt32(items[1], CultureInfo.InvariantCulture)
                        };

                        _desktopIconPositions.Add(items[2], point);
                    }
                }
            }

            catch (Exception ex)
            {
                Program.LogError(ex.Message);
                _desktopIconPositions = new Dictionary<string, Point>();
            }
        }

        public static void SaveIconPositions()
        {
            try
            {
                using (var isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
                using (var stream = new IsolatedStorageFileStream(IconPositionsFile, FileMode.Create, isoStore))
                using (var textStream = new StreamWriter(stream))
                {
                    foreach (var kvp in _desktopIconPositions)
                    {
                        var text = string.Format("{0} {1} {2}", kvp.Value.X, kvp.Value.Y, kvp.Key);
                        textStream.WriteLine(text);
                        Program.LogInformation(text);
                    }

                    textStream.Flush();
                    Program.LogInformation(IconPositionsFile + " saved");
                }
            }

            catch (Exception ex)
            {
                Program.LogError(ex.Message);
            }
        }
    }
}
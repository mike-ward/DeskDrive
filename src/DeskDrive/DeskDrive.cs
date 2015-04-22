// Copyright (c) 2011 Blue Onion Software, All rights reserved

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlueOnion.Properties;
using Microsoft.Win32;

namespace BlueOnion
{
    public partial class DeskDrive : Form
    {
        private readonly ShortcutCollection _shortcuts = new ShortcutCollection();
        public string RemoveableMediaDetectedAlert { get; private set; }

        public DeskDrive()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = 
            //    new System.Globalization.CultureInfo("el-GR");

            InitializeComponent();
            Localize();
            LoadSettings();
            Desktop.LoadIconPositions();

            CDCheckBox.CheckedChanged += CheckedChanged;
            removableCheckBox.CheckedChanged += CheckedChanged;
            fixedCheckBox.CheckedChanged += CheckedChanged;
            networkedCheckBox.CheckedChanged += CheckedChanged;
            ramCheckBox.CheckedChanged += CheckedChanged;
            hideTrayCheckBox.CheckedChanged += CheckedChanged;
            startupCheckBox.CheckedChanged += CheckedChanged;
            excludedTextBox.LostFocus += ExcludedTextBoxLostFocus;
            minimizeAllCheckBox.CheckedChanged += CheckedChanged;
            locusEffectCheckBox.CheckedChanged += CheckedChanged;
            rememberIconPositionsCheckBox.CheckedChanged += CheckedChanged;
            openExplorerCheckBox.CheckedChanged += CheckedChanged;
            remindRemoveMediaCheckBox.CheckedChanged += CheckedChanged;

            SetWorkingSetSize();
        }

        private void Localize()
        {
            try
            {
                using (var stream = File.OpenRead(Application.ExecutablePath + ".xml"))
                using (var xmlResourceManager = new XmlResourceManager(stream))
                {
                    Text = xmlResourceManager.GetString("DeskDrive");
                    notifyIcon.Text = xmlResourceManager.GetString("notifyIcon");
                    hideButton.Text = xmlResourceManager.GetString("hideButton");
                    label1.Text = xmlResourceManager.GetString("label1");
                    label2.Text = xmlResourceManager.GetString("label2");
                    label3.Text = xmlResourceManager.GetString("label3");
                    CDCheckBox.Text = xmlResourceManager.GetString("CDCheckBox");
                    removableCheckBox.Text = xmlResourceManager.GetString("RemovableCheckBox");
                    fixedCheckBox.Text = xmlResourceManager.GetString("FixedCheckBox");
                    networkedCheckBox.Text = xmlResourceManager.GetString("NetworkedCheckBox");
                    ramCheckBox.Text = xmlResourceManager.GetString("RamCheckBox");
                    label5.Text = xmlResourceManager.GetString("label5");
                    label6.Text = xmlResourceManager.GetString("label6");
                    linkLabel1.Text = xmlResourceManager.GetString("linkLabel1");
                    groupBox1.Text = xmlResourceManager.GetString("groupBox1");
                    label7.Text = xmlResourceManager.GetString("label7");
                    excludedTextBox.Text = xmlResourceManager.GetString("ExcludedTextBox");
                    label8.Text = xmlResourceManager.GetString("label8");
                    hideTrayCheckBox.Text = xmlResourceManager.GetString("HideTrayCheckBox");
                    startupCheckBox.Text = xmlResourceManager.GetString("StartupCheckBox");
                    showToolStripMenuItem.Text = xmlResourceManager.GetString("showToolStripMenuItem");
                    exitToolStripMenuItem.Text = xmlResourceManager.GetString("exitToolStripMenuItem");
                    minimizeAllCheckBox.Text = xmlResourceManager.GetString("minimizeAllCheckBox");
                    locusEffectCheckBox.Text = xmlResourceManager.GetString("locusEffectCheckBox");
                    rememberIconPositionsCheckBox.Text = xmlResourceManager.GetString("rememberIconPositionsCheckBox");
                    openExplorerCheckBox.Text = xmlResourceManager.GetString("openWindowsExplorerCheckBox");
                    RemoveableMediaDetectedAlert = xmlResourceManager.GetString("mediaDetectedAlert");
                    remindRemoveMediaCheckBox.Text = xmlResourceManager.GetString("checkForMedialCheckBox");
                }
            }

            catch (Exception ex)
            {
                Program.LogError(ex.Message);
            }
        }

        private void LoadSettings()
        {
            CDCheckBox.Checked = Settings.Default.CD;
            removableCheckBox.Checked = Settings.Default.Removable;
            fixedCheckBox.Checked = Settings.Default.Fixed;
            networkedCheckBox.Checked = Settings.Default.Networked;
            ramCheckBox.Checked = Settings.Default.Ram;
            excludedTextBox.Text = Settings.Default.Exclude;
            hideTrayCheckBox.Checked = Settings.Default.HideTrayIcon;
            startupCheckBox.Checked = Settings.Default.AutoStart;
            RegisterStartup(Settings.Default.AutoStart);
            minimizeAllCheckBox.Checked = Settings.Default.MinimizeAll;
            locusEffectCheckBox.Checked = Settings.Default.Locus;
            rememberIconPositionsCheckBox.Checked = Settings.Default.RememberIconPositions;
            openExplorerCheckBox.Checked = Settings.Default.OpenExplorer;
            remindRemoveMediaCheckBox.Checked = Settings.Default.RemindRemove;
        }

        private void SaveSettings()
        {
            Settings.Default.CD = CDCheckBox.Checked;
            Settings.Default.Removable = removableCheckBox.Checked;
            Settings.Default.Fixed = fixedCheckBox.Checked;
            Settings.Default.Networked = networkedCheckBox.Checked;
            Settings.Default.Ram = ramCheckBox.Checked;
            Settings.Default.Exclude = excludedTextBox.Text;
            Settings.Default.HideTrayIcon = hideTrayCheckBox.Checked;
            Settings.Default.AutoStart = startupCheckBox.Checked;
            RegisterStartup(Settings.Default.AutoStart);
            Settings.Default.MinimizeAll = minimizeAllCheckBox.Checked;
            Settings.Default.Locus = locusEffectCheckBox.Checked;
            Settings.Default.RememberIconPositions = rememberIconPositionsCheckBox.Checked;
            Settings.Default.OpenExplorer = openExplorerCheckBox.Checked;
            Settings.Default.RemindRemove = remindRemoveMediaCheckBox.Checked;
            Settings.Default.Save();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void ExcludedTextBoxLostFocus(object sender, EventArgs e)
        {
            if (excludedTextBox.Text != Settings.Default.Exclude)
                SaveSettings();
        }

        private static bool _checking;
        private bool _systemShuttingDown;

        private void CheckDrivesTimerTick(object sender, EventArgs e)
        {
            try
            {
                if (_checking)
                {
                    return;
                }
                _checking = true;

                // Additions...
                foreach (var drive in DriveInfo.GetDrives())
                {
                    try
                    {
                        if (DriveIncluded(drive) && drive.IsReady)
                        {
                            var name = GetDriveName(drive);

                            if (_shortcuts.Contains(name))
                                continue;

                            var desktopShortcutPath = DesktopShortcutPath(name + DriveInformation(drive));
                            Shell.CreateShortcut(desktopShortcutPath, drive.RootDirectory.FullName);
                            _shortcuts.Add(new Shortcut(desktopShortcutPath, drive.RootDirectory.FullName, name, drive.DriveType));
                            var location = Desktop.WaitForShortcut(name);
                            if (location == null)
                            {
                                continue;
                            }

                            if (Settings.Default.RememberIconPositions)
                            {
                                location = Desktop.LoadIconPosition(name) ?? location;
                                Desktop.SetIconPosition(name, (Point) location);
                            }

                            if (Settings.Default.MinimizeAll)
                                Shell.MinimizeAll();

                            if (Settings.Default.Locus)
                                Effects.ShowEffect(this, (Point) location);

                            if (Settings.Default.OpenExplorer)
                                NativeMethods.ShellExecute(IntPtr.Zero, "explore", desktopShortcutPath,
                                    "", "", NativeMethods.ShowCommands.SW_SHOWDEFAULT);
                        }
                    }

                    catch (Exception ex)
                    {
                        Program.LogError(ex.Message);
                    }
                }

                try
                {
                    // Subtractions
                    foreach (var shortcut in new List<Shortcut>(_shortcuts))
                    {
                        if (!Directory.Exists(shortcut.RootDirectoryPath) ||
                            !DriveIncluded(new DriveInfo(shortcut.RootDirectoryPath.Substring(0, 1))))
                        {
                            Desktop.SaveIconPosition(shortcut.Name, Desktop.GetIconPosition(shortcut.Name));
                            SafeFileDelete(shortcut.DesktopShortcutPath);
                            _shortcuts.Remove(shortcut.Name);
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Program.LogError(ex.Message);
                }
            }
            finally
            {
                _checking = false;
                SetWorkingSetSize();
            }
        }

        private static string GetDriveName(DriveInfo drive)
        {
            var name = drive.VolumeLabel;

            if (string.IsNullOrEmpty(name))
            {
                if (drive.Name[1] == Path.VolumeSeparatorChar)
                {
                    var key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\DriveIcons\" +
                              drive.Name[0] + @"\DefaultLabel";

                    name = Registry.LocalMachine.GetValue(key) as string ?? "";
                }

                if (string.IsNullOrEmpty(name))
                    name = "Desk Drive";
            }

            if (drive.Name[1] == Path.VolumeSeparatorChar)
                name += " (" + drive.Name[0] + ")";

            return name;
        }

        private bool DriveIncluded(DriveInfo drive)
        {
            if (drive == null)
                return false;

            if (Settings.Default.Exclude.Contains(Path.GetPathRoot(drive.RootDirectory.FullName)))
                return false;

            switch (drive.DriveType)
            {
                case DriveType.CDRom:
                    return CDCheckBox.Checked;
                case DriveType.Removable:
                    return removableCheckBox.Checked;
                case DriveType.Fixed:
                    return fixedCheckBox.Checked;
                case DriveType.Network:
                    return networkedCheckBox.Checked;
                case DriveType.Ram:
                    return ramCheckBox.Checked;
                default:
                    return false;
            }
        }

        private static string DesktopShortcutPath(string folder)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), folder + ".lnk");
        }

        private static string DriveInformation(DriveInfo drive)
        {
            var information = string.Empty;

            try
            {
                const double gb = 1024*1024*1024;

                if (drive.DriveType == DriveType.Removable)
                    information = string.Format(" - {0:0.0}GB ({1:0.0})", drive.TotalSize/gb, drive.TotalFreeSpace/gb);
            }

            catch (IOException)
            {
            }

            return information;
        }

        private void DeskDriveFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_systemShuttingDown)
            {
                _systemShuttingDown = false;
                e.Cancel = true;
                Task.Factory.StartNew(() =>
                     MessageBox.Show(
                         this,
                         RemoveableMediaDetectedAlert,
                         null,
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Warning));
            }
        }

        private void DeskDriveFormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var shortcut in _shortcuts)
            {
                Desktop.SaveIconPosition(shortcut.Name, Desktop.GetIconPosition(shortcut.Name));
                SafeFileDelete(shortcut.DesktopShortcutPath);
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void HideButtonClick(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Hide();
            SetWorkingSetSize();
        }

        private void ShowToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (Location.X >= Screen.PrimaryScreen.WorkingArea.Width ||
                Location.Y >= Screen.PrimaryScreen.WorkingArea.Height)
            {
                Location = new Point(200, 200);
            }

            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Show();
            BringToFront();
        }

        private static void SafeFileDelete(string path)
        {
            try
            {
                File.Delete(path);
            }

            catch (Exception ex)
            {
                Program.LogError(ex.Message);
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == NativeMethods.WM_NOTIFYDD)
            {
                ShowToolStripMenuItemClick(this, EventArgs.Empty);
                BringToFront();
                Activate();
            }

            else if (message.Msg == NativeMethods.WM_DEVICECHANGE)
            {
                var wpar = (uint) message.WParam;

                if (wpar == NativeMethods.DBT_DEVICEARRIVAL || wpar == NativeMethods.DBT_DEVICEREMOVECOMPLETE)
                    CheckDrivesTimerTick(this, EventArgs.Empty);
            }

            else if (message.Msg == NativeMethods.WM_QUERYENDSESSION)
            {
                if (Settings.Default.RemindRemove && RemoveableMediaPresent())
                {
                    NativeMethods.CancelShutdown();
                    _systemShuttingDown = true;
                    message.Result = new IntPtr(0);
                }
            }

            base.WndProc(ref message);
        }

        private void HideTrayCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            notifyIcon.Visible = !hideTrayCheckBox.Checked;
        }

        private static void RegisterStartup(bool register)
        {
            const string subkey = "DeskDriveStartup";
            var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (key == null)
                return;

            if (register)
                key.SetValue(subkey, Application.ExecutablePath, RegistryValueKind.String);

            else
                key.DeleteValue(subkey, false);
        }

        private static void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://mike-ward.net/deskdrive");
        }

        private static void SetWorkingSetSize()
        {
            var size = new UIntPtr(UInt32.MaxValue);
            NativeMethods.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, size, size);
        }

        private static void SetWorkingSetSizeTimerTick(object sender, EventArgs e)
        {
            SetWorkingSetSize();
        }

        public bool RemoveableMediaPresent()
        {
            return _shortcuts.Any(s => s.DriveType == DriveType.Removable || s.DriveType == DriveType.CDRom);
        }
    }
}
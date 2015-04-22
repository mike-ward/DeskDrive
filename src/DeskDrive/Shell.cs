// Copyright 2008 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    using System.Runtime.InteropServices;

    static class Shell
    {
        static public void CreateShortcut(string shortcutPath, string targetPath)
        {
            WshShell shell = null;
            IWshShell shellInterface = null;
            IWshShortcut shortcut = null;

            try
            {
                shell = new WshShell();
                shellInterface = (IWshShell)shell;
                shortcut = shellInterface.CreateShortcut(shortcutPath);
                shortcut.Description = targetPath;
                shortcut.TargetPath = targetPath;
                shortcut.IconLocation = targetPath + ",0";
                shortcut.Save();
            }

            finally
            {
                Release(shortcut);
                Release(shellInterface);
                Release(shell);
            }
        }

        static public void MinimizeAll()
        {
            Shell32 shell = null;
            IShellDispatch shellDispatch = null;

            try
            {
                shell = new Shell32();
                shellDispatch = (IShellDispatch)shell;
                shellDispatch.MinimizeAll();
            }

            finally
            {
                Release(shellDispatch);
                Release(shell);
            }
        }

        static void Release(object comObject)
        {
            if (comObject != null)
                Marshal.ReleaseComObject(comObject);
        }
    }
}

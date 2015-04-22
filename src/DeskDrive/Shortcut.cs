// Copyright (c) 2008 Blue Onion Software
// All rights reserved

using System.IO;

namespace BlueOnion
{
    using System.Collections.ObjectModel;

    class Shortcut
    {
        public string DesktopShortcutPath { get; private set; }
        public string RootDirectoryPath { get; private set; }
        public string Name { get; private set; }
        public DriveType DriveType { get; private set; }

        public Shortcut(string desktopShortcutPath, string rootDirectoryPath, string name, DriveType driveType)
        {
            Throw.IfNullOrEmpty(desktopShortcutPath, "desktopShortcutPath");
            Throw.IfNullOrEmpty(rootDirectoryPath, "rootDirectoryPath");
            Throw.IfNullOrEmpty(name, "name");

            DesktopShortcutPath = desktopShortcutPath;
            RootDirectoryPath = rootDirectoryPath;
            Name = name;
            DriveType = driveType;
        }
    }

    class ShortcutCollection : KeyedCollection<string, Shortcut>
    {
        protected override string GetKeyForItem(Shortcut item)
        {
            return item.Name;
        }
    }
}

// Copyright 2008 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("13709620-C279-11CE-A49E-444553540000")]
    class Shell32
    {
    }

    [ComImport]
    [Guid("D8F015C0-C278-11CE-A49E-444553540000")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    interface IShellDispatch
    {
        [DispId(0x60020007)]
        void MinimizeAll();

        [DispId(0x60020008)]
        void UndoMinimizeAll();
    }
}

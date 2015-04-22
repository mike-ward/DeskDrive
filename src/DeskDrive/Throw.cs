// Copyright (C) 2007 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    // ReSharper disable LocalizableElement
    using System;

    static class Throw
    {
        const string NotSpecified = "not specified";

        static internal void IfNullOrEmpty(string arg, string name)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw (arg == null)
                    ? new ArgumentNullException(name ?? NotSpecified)
                    : new ArgumentException("empty", name ?? NotSpecified);
            }
        }
    }
}

// Copyright (c) 2008 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Resources;

    // ReSharper disable LocalizableElement

    public sealed class XmlResourceManager : ResourceManager, IDisposable
    {
        private Stream _stream;
        private readonly Hashtable _resources;

        public XmlResourceManager(Stream resourceStream)
        {
            if (resourceStream == null)
                throw new ArgumentNullException("resourceStream");

            if (resourceStream.CanRead == false)
                throw new ArgumentException("Not readable", "resourceStream");

            if (resourceStream.CanSeek == false)
                throw new ArgumentException("Not seekable", "resourceStream");

            _stream = resourceStream;
            _resources = new Hashtable();
        }

        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            if (_resources.Contains(culture.Name) == false && createIfNotExists)
            {
                _stream.Position = 0;
                _resources.Add(culture.Name, new XmlResourceSet(_stream, culture));
            }

            return (XmlResourceSet)_resources[culture.Name];
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }
        }
    }
}

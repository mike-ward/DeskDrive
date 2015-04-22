namespace BlueOnion
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Resources;
    using System.Xml;

    public sealed class XmlResourceReader : IResourceReader
    {
        Hashtable resources;
        CultureInfo culture;

        public XmlResourceReader(Stream stream, CultureInfo cultureInfo)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (stream.CanRead == false)
                throw new ArgumentException("stream is not readable", "stream");

            if (stream.CanSeek == false)
                throw new ArgumentException("stream is not seekable", "stream");

            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            resources = new Hashtable();
            culture = cultureInfo;
            ReadResources(stream);
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            resources = null;
            culture = null;
            GC.SuppressFinalize(this);
        }

        void ReadResources(Stream stream)
        {
            using (var reader = XmlReader.Create(stream))
            {
                reader.Read();
                reader.ReadStartElement("resources");
                reader.ReadStartElement("strings");

                while (reader.IsStartElement())
                    AddString(reader, resources);
            }
        }

        void AddString(XmlReader reader, Hashtable strings)
        {
            string key = reader.Name;

            if (strings.ContainsKey(key))
                throw new InvalidDataException("Already contains key (" + key + ")");

            reader.Read();

            while (reader.IsStartElement())
            {
                string name = reader.Name;
                string value = reader.ReadElementString();

                if (string.IsNullOrEmpty(value))
                    continue;

                if (name == culture.Name)
                    strings.Add(key, value);

                if (name == "default" && culture.Equals(CultureInfo.InvariantCulture))
                    strings.Add(key, value);
            }

            reader.ReadEndElement();
        }
    }
}

namespace BlueOnion
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Resources;

    public class XmlResourceSet : ResourceSet
    {
        public XmlResourceSet(Stream resourceStream, CultureInfo cultureInfo)
            : base(new XmlResourceReader(resourceStream, cultureInfo))
        {
        }

        public override Type GetDefaultReader()
        {
            return typeof(XmlResourceReader);
        }
    }
}

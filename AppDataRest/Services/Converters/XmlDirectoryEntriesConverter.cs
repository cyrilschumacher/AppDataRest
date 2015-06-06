using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace AppDataRest.Services.Converters
{
    /// <summary>
    ///     Converts directory entries to Xml data.
    /// </summary>
    [CLSCompliant(true)]
    public class XmlDirectoryEntriesConverter : IDirectoryEntriesConverter
    {
        /// <summary>
        ///     Converts entries in a XML document.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <returns>The XML document that represents the entries.</returns>
        public string Convert(IEnumerable<string> entries)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            var root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            var rootElement = doc.CreateElement("directory");
            foreach (var entry in entries)
            {
                var element = doc.CreateElement("entry");
                var textNode = doc.CreateTextNode(entry);
                element.AppendChild(textNode);
                rootElement.AppendChild(element);
            }

            doc.AppendChild(rootElement);

            using (var stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                doc.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}

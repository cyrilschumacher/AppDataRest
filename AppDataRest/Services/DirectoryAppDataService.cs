using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AppDataRest.Services.Converters;

namespace AppDataRest.Services
{
    [CLSCompliant(true)]
    public class DirectoryAppDataService : BaseAppDataService
    {
        #region Properties.

        /// <summary>
        ///     Gets or sets the converters.
        /// </summary>
        public IDictionary<string, IDirectoryEntriesConverter> Converters { get; set; }  

        #endregion Properties.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="appDataPath">The "App_Data" directory path.</param>
        public DirectoryAppDataService(string appDataPath) : base(appDataPath)
        {
            // Initializes properties.
            Converters = new Dictionary<string, IDirectoryEntriesConverter>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"json", new JsonDirectoryEntriesConverter()},
                {"xml", new XmlDirectoryEntriesConverter()}
            };
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates section.

        /// <summary>
        ///     Formats the file system entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>The formatted entry.</returns>
        private static string _FormatFileSystemEntry(string entry)
        {
            var extensionItem = Path.GetExtension(entry);
            return string.IsNullOrEmpty(extensionItem)
                ? entry
                : entry.Replace(extensionItem, string.Empty) + "/" + extensionItem.Trim('.');
        }

        #endregion Private section.

        /// <summary>
        ///     Gets the directory content.
        /// </summary>
        /// <param name="relativePath">The directory path.</param>
        /// <param name="dataFormat">The data format.</param>
        /// <returns>The directory content.</returns>
        public override string GetContent(string relativePath, string dataFormat)
        {
            if (Converters.ContainsKey(dataFormat))
            {
                var path = Path.Combine(appDataPath, relativePath);
                var entries = Directory.GetFileSystemEntries(path);
                var value = entries.Select(entry => entry.Replace(path, string.Empty).Replace('\\', '/'));
                var items = value.Select(_FormatFileSystemEntry);

                var converter = Converters[dataFormat];
                return converter.Convert(items);
            }

            return string.Empty;
        }

        #endregion Methods section.
    }
}

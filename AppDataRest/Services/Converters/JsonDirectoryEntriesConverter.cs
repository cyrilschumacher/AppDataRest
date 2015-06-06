using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppDataRest.Services.Converters
{
    /// <summary>
    ///     Converts directory entries to JSON data.
    /// </summary>
    [CLSCompliant(true)]
    public class JsonDirectoryEntriesConverter : IDirectoryEntriesConverter
    {
        /// <summary>
        ///     Converts entries in a JSON data.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <returns>The JSON data that represents the entries.</returns>
        public string Convert(IEnumerable<string> entries)
        {
            return JsonConvert.SerializeObject(entries);
        }
    }
}

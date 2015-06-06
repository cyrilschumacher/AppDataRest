using System;
using System.Collections.Generic;

namespace AppDataRest.Services.Converters
{
    /// <summary>
    ///     Directory entries converter interface.
    /// </summary>
    [CLSCompliant(true)]
    public interface IDirectoryEntriesConverter
    {
        /// <summary>
        ///     Converts entries in a <see cref="string"/>.
        /// </summary>
        /// <param name="entries">The entries.</param>
        /// <returns>A <see cref="string"/> that represents the entries.</returns>
        string Convert(IEnumerable<string> entries);
    }
}

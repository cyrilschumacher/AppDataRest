using System;
using System.IO;

namespace AppDataRest.Services
{
    [CLSCompliant(true)]
    public class FileAppDataService : BaseAppDataService
    {
        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="appDataPath">The "App_Data" directory path.</param>
        public FileAppDataService(string appDataPath)
            : base(appDataPath)
        {
        }

        #endregion Constructors section.

        #region Methods section.

        /// <summary>
        ///     Gets the file content.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="dataFormat">The data format.</param>
        /// <returns>The file content.</returns>
        public override string GetContent(string filePath, string dataFormat)
        {
            var path = Path.Combine(appDataPath, filePath);
            path += string.Concat(".", dataFormat);

            return File.ReadAllText(path);
        }
        
        #endregion Methods section.
    }
}

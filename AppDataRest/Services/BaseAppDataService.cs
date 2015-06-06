using System;
using System.IO;
using System.Linq;

namespace AppDataRest.Services
{
    [CLSCompliant(true)]
    public abstract class BaseAppDataService
    {
        #region Members section.

        /// <summary>
        ///     "App_Data" directory path.
        /// </summary>
        protected readonly string appDataPath;

        #endregion Members section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="appDataPath">The "App_Data" directory path.</param>
        protected BaseAppDataService(string appDataPath)
        {
            if (string.IsNullOrWhiteSpace(appDataPath))
            {
                throw new ArgumentNullException("appDataPath");
            }

            this.appDataPath = appDataPath;
        }

        #endregion Constructors section.

        #region Methods section.

        /// <summary>
        ///     Gets content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="dataFormat">The data format.</param>
        /// <returns>The content.</returns>
        public abstract string GetContent(string path, string dataFormat);

        /// <summary>
        ///     Gets and removes extension of the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="defaultExtension">The default extensino.</param>
        /// <returns>The extension.</returns>
        public static string GetAndRemoveExtension(ref string path, string defaultExtension)
        {
            if (string.IsNullOrEmpty(path))
            {
                return defaultExtension;
            }

            var pathSplit = path.Split('/').ToList();
            var extension = pathSplit.Last();
            pathSplit.RemoveAt(pathSplit.Count - 1);

            path = string.Join(@"\", pathSplit);
            return string.IsNullOrEmpty(extension) ? defaultExtension : extension;
        }

        /// <summary>
        ///     Determines if the path represents a directory.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>True if the paths represents a directory, False, otherwise.</returns>
        public static bool IsDirectory(string absolutePath, string relativePath)
        {
            var path = Path.Combine(absolutePath, relativePath);
            return Directory.Exists(path);
        }

        /// <summary>
        ///     Determines if the path represents a file.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="extension">The file extension.</param>
        /// <returns>True if the paths represents a file, False, otherwise.</returns>
        public static bool IsFile(string absolutePath, string relativePath, string extension)
        {
            var filePath = string.Concat(relativePath, ".", extension);
            var path = Path.Combine(absolutePath, filePath);
            return File.Exists(path);
        }

        #endregion Methods section.
    }
}

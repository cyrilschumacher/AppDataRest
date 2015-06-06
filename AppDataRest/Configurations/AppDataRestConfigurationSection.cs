using System;
using System.Configuration;
using AppDataRest.Configurations.Elements;

namespace AppDataRest.Configurations
{
    /// <summary>
    ///     The "AppDataRest" configuration section.
    /// </summary>
    [CLSCompliant(true)]
    public class AppDataRestConfigurationSection : ConfigurationSection
    {
        #region Properties.

        /// <summary>
        ///     Gets or sets the data format attribute.
        /// </summary>
        [ConfigurationProperty("directory")]
        public DirectoryElement Directory
        {
            get
            {
                return this["directory"] as DirectoryElement;
            }
            set
            {
                this["directory"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the path attribute.
        /// </summary>
        [ConfigurationProperty("path")]
        public PathElement Path
        {
            get
            {
                return this["path"] as PathElement;
            }
            set
            {
                this["path"] = value;
            }
        }

        #endregion Properties section.
    }
}

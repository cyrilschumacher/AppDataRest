using System;
using System.Configuration;

namespace AppDataRest.Configurations.Elements
{
    /// <summary>
    ///     Element for the data format directory.
    /// </summary>
    [CLSCompliant(true)]
    public class DirectoryElement : ConfigurationElement
    {
        #region Properties.

        /// <summary>
        ///     Gets or sets the default format for displaying directory entries.
        /// </summary>
        /// <value>The default value.</value>
        [ConfigurationProperty("defaultDataFormat", DefaultValue = "JSON")]
        public string DefaultDataFormat
        {
            get
            {
                return this["defaultDataFormat"] as string;
            }
            set
            {
                this["defaultDataFormat"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the data format attribute.
        /// </summary>
        [ConfigurationProperty("converters", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConverterElement), AddItemName = "converter")]
        public ConvertersCollection Converters
        {
            get
            {
                return this["converters"] as ConvertersCollection;
            }
            set
            {
                this["converters"] = value;
            }
        }

        #endregion Properties.
    }
}

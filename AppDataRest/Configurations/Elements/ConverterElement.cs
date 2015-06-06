using System;
using System.ComponentModel;
using System.Configuration;

namespace AppDataRest.Configurations.Elements
{
    /// <summary>
    ///     Converter informations configuration element.
    /// </summary>
    [CLSCompliant(true)]
    public class ConverterElement : ConfigurationElement
    {
        #region Properties.

        /// <summary>
        ///     Gets or sets the format name.
        /// </summary>
        /// <value>The format name.</value>
        [ConfigurationProperty("format", IsRequired = true)]
        public string Format
        {
            get
            {
                return this["format"] as string;
            }
            set
            {
                this["format"] = value;
            }
        }

        /// <summary>
        ///     Gets or sets the converter type.
        /// </summary>
        /// <value>The converter type.</value>
        [ConfigurationProperty("type", IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Type
        {
            get
            {
                return this["type"] as Type;
            }
            set
            {
                this["type"] = value;
            }
        }

        #endregion Properties.
    }
}
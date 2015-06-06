using System;
using System.Configuration;

namespace AppDataRest.Configurations.Elements
{
    /// <summary>
    ///     Path informations configuration element.
    /// </summary>
    [CLSCompliant(true)]
    public class PathElement : ConfigurationElement
    {
        #region Properties.

        /// <summary>
        ///     Gets or sets the root path.
        /// </summary>
        /// <value>The root path.</value>
        [ConfigurationProperty("root", DefaultValue = "")]
        public string Root
        {
            get
            {
                return this["root"] as string;
            }
            set
            {
                this["root"] = value;
            }
        }
        
        #endregion Properties.
    }
}

using System;
using System.Configuration;

namespace AppDataRest.Configurations.Elements
{
    /// <summary>
    ///     Converters collection.
    /// </summary>
    [CLSCompliant(true)]
    public class ConvertersCollection : ConfigurationElementCollection
    {
        #region Properties section.

        /// <summary>
        ///     Gets the element by a index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The element.</returns>
        public ConverterElement this[int index]
        {
            get { return BaseGet(index) as ConverterElement; }
        }

        #endregion Properties section.

        #region Methods.

        /// <summary>
        ///     Creates a new element.
        /// </summary>
        /// <returns>The new element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConverterElement();
        }

        /// <summary>
        ///     Gets the element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The element key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConverterElement) element).Format;
        }

        #endregion Methods.
    }
}
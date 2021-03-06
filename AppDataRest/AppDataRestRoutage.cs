﻿using System;
using System.Globalization;
using System.Web.Http;

namespace AppDataRest
{
    /// <summary>
    ///     AppDataRest routage.
    /// </summary>
    [CLSCompliant(true)]
    public static class AppDataRestRoutage
    {
        #region Constants section.

        /// <summary>
        ///     Route template.
        /// </summary>
        private const string RouteTemplate = "{0}/{{*path}}";

        #endregion Constants section.

        #region Methods section.

        /// <summary>
        ///     Registers configuration.
        /// </summary>
        /// <param name="config">The HTTP configuration.</param>
        /// <param name="rootName">The route name.</param>
        public static void Register(HttpConfiguration config, string rootName)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(rootName))
            {
                throw new ArgumentNullException("rootName");
            }

            var routeTemplate = string.Format(CultureInfo.InvariantCulture, RouteTemplate, rootName);
            config.Routes.MapHttpRoute("AppDataRest", routeTemplate, new { controller = "AppDataRest" });
        }

        #endregion Methods section.
    }
}
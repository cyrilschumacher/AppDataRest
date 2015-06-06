using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using AppDataRest.Configurations;
using AppDataRest.Configurations.Elements;
using AppDataRest.Services;
using AppDataRest.Services.Converters;

namespace AppDataRest.Controllers
{
    /// <summary>
    ///     AppDataRest controller.
    /// </summary>
    [CLSCompliant(true)]
    public class AppDataRestController : ApiController
    {
        #region Constants section.

        /// <summary>
        ///     Absolute path to the application data directory.
        /// </summary>
        private const string AppDataPath = "~/App_Data";

        #endregion Constants section.

        #region Members section.

        /// <summary>
        ///     Absolute path of Application Data directory.
        /// </summary>
        private readonly string _absolutePath;

        /// <summary>
        ///     Configuration.
        /// </summary>
        private readonly AppDataRestConfigurationSection _configuration;

        /// <summary>
        ///     File service.
        /// </summary>
        private readonly BaseAppDataService _fileService;

        /// <summary>
        ///     Directory service.
        /// </summary>
        private readonly DirectoryAppDataService _directoryService;

        #endregion Members section.

        #region Constructors section.

        /// <summary>
        ///     Constructor.
        /// </summary>
        public AppDataRestController()
        {
            // Initialize members.
            // Initialize, first, configuration for be used by the services and the resolution of absolute path.
            _configuration = _GetConfiguration();
            _absolutePath = _GetAbsolutePath(_configuration.Path.Root);
            _fileService = new FileAppDataService(_absolutePath);
            _directoryService = new DirectoryAppDataService(_absolutePath);

            // Adds converters.
            foreach (ConverterElement element in _configuration.Directory.Converters)
            {
                var converter = Activator.CreateInstance(element.Type) as IDirectoryEntriesConverter;
                var item = new KeyValuePair<string, IDirectoryEntriesConverter>(element.Format, converter);

                _directoryService.Converters.Add(item);
            }
        }

        #endregion Constructors section.

        #region Methods section.

        #region Privates.

        /// <summary>
        ///     Creates a HTTP response.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="contentType">The content-type.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The HTTP response.</returns>
        private HttpResponseMessage _CreateHttpResponse(string content, string contentType, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("ContentType", contentType);
            response.Content = new StringContent(content, encoding);

            return response;
        }

        /// <summary>
        ///     Gets the absolute path.
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>The "App_Data" absolute path.</returns>
        private static string _GetAbsolutePath(string relativePath)
        {
            var path = Path.Combine(AppDataPath, relativePath);
            return HostingEnvironment.MapPath(path);
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        /// <returns>The configuration.</returns>
        private static AppDataRestConfigurationSection _GetConfiguration()
        {
            return ConfigurationManager.GetSection("appDataRestGroup/appDataRest") as AppDataRestConfigurationSection;
        }

        /// <summary>
        ///     Gets the content type by extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>The content type.</returns>
        private static string _GetContentType(string extension)
        {
            var contentType = "text/plain";
            switch (extension.ToLowerInvariant())
            {
                case "json":
                    contentType = "application/json";
                    break;
                case "xml":
                    contentType = "application/xml";
                    break;
            }
            return contentType;
        }

        #endregion Privates.

        /// <summary>
        ///     Gets file content (or directory items).
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file content (or directory items).</returns>
        public HttpResponseMessage Get([FromUri] string path)
        {
            // If the path has a null value, it resets the value to a empty string.
            path = path ?? string.Empty;

            // Obtains the extension (if exists) and removes it of path.
            var extension = BaseAppDataService.GetAndRemoveExtension(ref path, _configuration.Directory.DefaultDataFormat);

            BaseAppDataService service;
            if (BaseAppDataService.IsDirectory(_absolutePath, path))
            {
                service = _directoryService;
            }
            else if (BaseAppDataService.IsFile(_absolutePath, path, extension))
            {
                service = _fileService;
            }
            else
            {
                // If the path doesn't represents a file or directory,
                // it return a HTTP code for indicates no content.
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            // Gets content and determines the type of content.
            var content = service.GetContent(path, extension);
            var contentType = _GetContentType(extension);

            // Returns the content to the user.
            return _CreateHttpResponse(content, contentType);
        }

        #endregion Methods section.
    }
}
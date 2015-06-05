using System.Globalization;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml;

namespace AppDataRest.Controllers
{
    /// <summary>
    ///     AppDataRest controller.
    /// </summary>
    public class AppDataRestController : ApiController
    {
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
        ///     Gets the "App_Data" absolute path.
        /// </summary>
        /// <returns>The "App_Data" absolute path.</returns>
        private static string _GetAppDataPath()
        {
            return HostingEnvironment.MapPath("~/App_Data");
        }

        /// <summary>
        ///     Gets directory items.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="dataFormat">The data format.</param>
        /// <returns>The items of directory.</returns>
        private HttpResponseMessage _GetDirectoryItems(string directoryPath, string dataFormat)
        {
            var entries = Directory.GetFileSystemEntries(directoryPath);
            var value = entries.Select(entry => entry.Replace(directoryPath, string.Empty).Replace('\\', '/'));

            var items = value.Select(item =>
            {
                var extensionItem = Path.GetExtension(item);
                if (string.IsNullOrEmpty(extensionItem))
                {
                    return item;
                }

                return item.Replace(extensionItem, string.Empty) + "/" + extensionItem.Trim('.');
            });

            string content;
            var contentType = "application/json; charset=utf-8";
            switch (dataFormat)
            {
                case "xml":
                    var doc = new XmlDocument();
                    var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    var root = doc.DocumentElement;
                    doc.InsertBefore(xmlDeclaration, root);

                    var rootElement = doc.CreateElement("AppData");
                    foreach (var item in items)
                    {
                        var element = doc.CreateElement("item");
                        var textNode = doc.CreateTextNode(item);
                        element.AppendChild(textNode);
                        rootElement.AppendChild(element);
                    }
                    doc.AppendChild(rootElement);

                    using (var stringWriter = new StringWriter(CultureInfo.InvariantCulture))
                    using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                    {
                        doc.WriteTo(xmlTextWriter);
                        xmlTextWriter.Flush();
                        content = stringWriter.GetStringBuilder().ToString();
                    }

                    contentType = "text/xml; charset=utf-8";
                    break;

                default:
                    content = JsonConvert.SerializeObject(items);
                    break;
            }

            return _CreateHttpResponse(content, contentType);
        }

        /// <summary>
        ///     Gets a file content.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="extension">The file extension.</param>
        /// <returns>The file content.</returns>
        private HttpResponseMessage _GetFileContent(string filePath, string extension)
        {
            // Reads file.
            var content = File.ReadAllText(filePath);

            // Gets Content-Type.
            var contentType = "text/plain; charset=utf-8";
            switch (extension)
            {
                case "json":
                    contentType = "application/json; charset=utf-8";
                    break;

                case "xml":
                    contentType = "application/xml; charset=utf-8";
                    break;
            }

            return _CreateHttpResponse(content, contentType);
        }

        #endregion Privates.

        /// <summary>
        ///     Gets file content (or directory items).
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file content (or directory items).</returns>
        public HttpResponseMessage Get(string path)
        {
            // Gets absolute path of "App_Data" directory.
            var appDataAbsolutePath = _GetAppDataPath();
            if (appDataAbsolutePath == null)
            {
                throw new IOException("The root directory doesn't exists.");
            }

            // Gets extension.
            var pathSplit = path.Split('/').ToList();
            var extension = pathSplit.Last();
            pathSplit.RemoveAt(pathSplit.Count - 1);
            extension = extension.ToLowerInvariant();

            // Creates relative and absolute paths.
            var relativePath = string.Join(@"\", pathSplit);
            var absolutePath = Path.Combine(appDataAbsolutePath, relativePath);

            // Creates file relative and absolute path.
            var filePath = string.Concat(relativePath, ".", extension);
            var fileAbsolutePath = Path.Combine(appDataAbsolutePath, filePath);

            // Gets the file content.
            if (File.Exists(fileAbsolutePath))
            {
                return _GetFileContent(fileAbsolutePath, extension);
            }

            // Gets the directory content.
            if (Directory.Exists(absolutePath))
            {
                return _GetDirectoryItems(absolutePath, extension);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        #endregion Methods section.
    }
}
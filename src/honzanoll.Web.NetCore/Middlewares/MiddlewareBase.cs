using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using honzanoll.Web.Middlewares.Exceptions;
using System;
using System.Threading.Tasks;

namespace honzanoll.Web.Middlewares
{
    /// <summary>
    /// Base middleware
    /// </summary>
    public abstract class MiddlewareBase : IMiddleware
    {
        #region Public methods

        public abstract Task InvokeAsync(HttpContext httpContext, RequestDelegate next);

        #endregion

        #region Protected methods

        /// <summary>
        /// Handle catch error
        /// </summary>
        /// <param name="e">Catch error</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="httpContext">Current http context</param>
        /// <returns></returns>
        protected async Task HandleErrorAsync(Exception e, ILogger logger, HttpContext httpContext)
        {
            if (e is MiddlewareException)
            {
                logger.LogWarning(e.ToString());

                if (e is UnauthorizedException)
                    await MakeTextResponseAsync(httpContext, "Neautorizovana akce.");
                else if (e is UnknownActionException)
                    await MakeTextResponseAsync(httpContext, "Neplatna akce.");
                else if (e is MissingParameterException)
                    await MakeTextResponseAsync(httpContext, "Chybejici parametr.");
                else if (e is UnknownDocumentException)
                    await MakeTextResponseAsync(httpContext, "Neexistujici dokument.");
                else
                    await MakeTextResponseAsync(httpContext, "Neco se pokazilo.");
            }
            else
            {
                logger.LogError(e.ToString());
                await MakeTextResponseAsync(httpContext, "Neco se pokazilo.");
            }
        }

        /// <summary>
        /// Make text response
        /// </summary>
        /// <param name="httpContext">Current http context</param>
        /// <param name="response">Text response</param>
        /// <returns></returns>
        protected async Task MakeTextResponseAsync(HttpContext httpContext, string response)
        {
            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync(response);
            return;
        }

        /// <summary>
        /// Send file in response
        /// </summary>
        /// <param name="httpContext">Current http context</param>
        /// <param name="response">The file to send</param>
        /// <param name="contentType">The file content type</param>
        /// <param name="fileName">The file name</param>
        /// <param name="appendFileExtension">Append file extension to name</param>
        /// <returns></returns>
        protected async Task MakeFileResponseAsync(HttpContext httpContext, byte[] response, string contentType, string fileName, bool appendFileExtension = true)
        {
            string responseFileName = Uri.EscapeDataString(fileName);
            if (appendFileExtension)
                responseFileName += GetFileExtension(contentType);

            httpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename*=UTF-8''{responseFileName}");
            httpContext.Response.ContentType = contentType;
            await httpContext.Response.Body.WriteAsync(response, 0, response.Length);

            return;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get file extension
        /// </summary>
        /// <param name="contentType">The file content type</param>
        /// <returns>The file extension</returns>
        private string GetFileExtension(string contentType)
        {
            switch (contentType)
            {
                case "application/pdf": return ".pdf";
                default: return string.Empty;
            }
        }

        #endregion
    }
}

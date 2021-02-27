using Microsoft.AspNetCore.Http;
using honzanoll.Web.Middlewares.Consts;
using honzanoll.Web.Middlewares.Exceptions;

namespace honzanoll.Web.Middlewares.Extensions
{
    /// <summary>
    /// Common middleware extensions
    /// </summary>
    public static class HttpContextExtensions
    {
        #region Public methods

        /// <summary>
        /// Ensure valid parameters
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="parameterName">Parameter name</param>
        /// <returns>Parameter value</returns>
        public static string EnsureValidParameter(this HttpContext httpContext, string parameterName)
        {
            string parameter = httpContext.Request.Query[parameterName];
            if (!string.IsNullOrWhiteSpace(parameter))
                return parameter;

            throw new MissingParameterException(parameterName);
        }

        /// <summary>
        /// Ensure valid identifier in parameter
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>Identifier value</returns>
        public static int EnsureValidIdParameter(this HttpContext httpContext)
        {
            if (int.TryParse(httpContext.Request.Query[Parameters.Id], out int id))
                return id;

            throw new MissingParameterException(nameof(Parameters.Id));
        }

        #endregion
    }
}

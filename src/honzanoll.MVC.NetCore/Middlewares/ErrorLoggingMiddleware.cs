using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Threading.Tasks;

namespace honzanoll.MVC.NetCore.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        #region Fields

        private readonly RequestDelegate next;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructors

        public ErrorLoggingMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        #endregion

        #region Public methods

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                logger.Error(e.ToString());
                throw;
            }
        }

        #endregion
    }
}

using Microsoft.AspNetCore.Builder;
using honzanoll.MVC.NetCore.Middlewares;

namespace honzanoll.MVC.NetCore.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        #region Public methods

        public static void UseErrorLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorLoggingMiddleware>();
        }

        #endregion
    }
}

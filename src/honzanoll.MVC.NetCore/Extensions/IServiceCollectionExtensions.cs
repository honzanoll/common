using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using honzanoll.MVC.NetCore.Services;
using honzanoll.MVC.NetCore.Services.Abstractions;

namespace honzanoll.MVC.NetCore.Extensions
{
    public static class IServiceCollectionExtensions
    {
        #region Public methods

        public static void AddViewRenderer(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IViewRendererService, ViewRendererService>();
        }

        #endregion
    }
}

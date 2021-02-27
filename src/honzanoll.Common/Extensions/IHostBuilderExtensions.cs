using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.Common.Extensions
{
    public static class IHostBuilderExtensions
    {
        #region Public methods

        public static IHostBuilder ConfigureNCAppConfiguration(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.Sources.Clear();

                configurationBuilder.SetBasePath(webHostBuilderContext.HostingEnvironment.ContentRootPath);
                configurationBuilder.AddJsonFile(IConfigurationExtensions.GetAppSettingsFileRelativePath(), false, true);
            });
        }

        public static IHostBuilder ConfigureNCAppEnvConfiguration(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.Sources.Clear();

                configurationBuilder.SetBasePath(webHostBuilderContext.HostingEnvironment.ContentRootPath);
                configurationBuilder.AddJsonFile(IConfigurationExtensions.GetAppSettingsFileRelativePath(webHostBuilderContext.HostingEnvironment), false, true);

                configurationBuilder.AddEnvironmentVariables();
            });
        }

        public static IHostBuilder ConfigureNCAppConfiguration(this IHostBuilder hostBuilder, Func<string> getAppSettingsFileRelativePath)
        {
            return hostBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(webHostBuilderContext.HostingEnvironment.ContentRootPath);
                configurationBuilder.AddJsonFile(getAppSettingsFileRelativePath(), false, false);
            });
        }

        public static IHostBuilder ConfigureNCHostConfiguration(this IHostBuilder hostBuilder, string appSettingsFileRelativePath)
        {
            hostBuilder.ConfigureHostConfiguration(configurationBuilder =>
            {
                configurationBuilder.Sources.Clear();

                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configurationBuilder.AddJsonFile(appSettingsFileRelativePath, false, true);
            });

            return hostBuilder;
        }

        public static Task RunAsServiceAsync(this IHostBuilder hostBuilder, CancellationToken cancellationToken = default(CancellationToken))
        {
            return hostBuilder.Build().RunAsync();
        }

        #endregion
    }
}

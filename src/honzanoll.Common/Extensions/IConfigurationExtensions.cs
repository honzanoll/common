using Microsoft.Extensions.Hosting;

namespace honzanoll.Common.Extensions
{
    public static class IConfigurationExtensions
    {
        #region Public methods

        public static string GetAppSettingsFileRelativePath()
        {
#if DEV
            return "appsettings.Dev.json";

#elif TEST
            return "appsettings.Test.json";

#elif PROD
            return "appsettings.Prod.json";

#elif DEBUG
            return "appsettings.Development.json";

#else
            return "appsettings.json";
#endif
        }

        public static string GetAppSettingsFileRelativePath(IHostEnvironment hostEnvironment)
        {
            return $"appsettings.{hostEnvironment.EnvironmentName}.json";
        }

        public static string GetConfigurationName()
        {
#if DEV
            return "Dev";

#elif TEST
            return "Test";

#elif PROD
            return "Prod";

#elif DEBUG
            return "Debug";

#else
            return "Unknown";
#endif
        }

        #endregion
    }
}

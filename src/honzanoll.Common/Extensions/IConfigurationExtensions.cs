using Microsoft.Extensions.Hosting;

namespace honzanoll.Common.Extensions
{
    public static class IConfigurationExtensions
    {
        #region Public methods

        public static string GetAppSettingsFileRelativePath()
        {
#if DEBUG
            return "appsettings.localhost.json";
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
#if DEBUG
            return "Debug";
#else
            return "Unknown";
#endif
        }

        #endregion
    }
}

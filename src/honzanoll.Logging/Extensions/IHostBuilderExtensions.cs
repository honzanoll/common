using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace honzanoll.Logging.Extensions
{
    public static class IHostBuilderExtensions
    {
        #region Public methods

        public static IHostBuilder ConfigureNCLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseNLog();
        }

        #endregion
    }
}

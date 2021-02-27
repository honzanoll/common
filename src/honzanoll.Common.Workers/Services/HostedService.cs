using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.Common.Workers.Services
{
    public abstract class HostedService : IHostedService
    {
        #region Fields

        private readonly ILogger<HostedService> logger;

        #endregion

        #region Protected members

        protected abstract string ServiceName { get; }

        #endregion

        #region Constructors

        public HostedService(ILogger<HostedService> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public methods

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{ServiceName} starting...");
            try
            {
                await OnStartingAsync(cancellationToken);
                await OnStartedAsync(cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogCritical(e, $"{nameof(HostedService)}: {ServiceName}");
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{nameof(HostedService)}: {ServiceName} stopping...");

            try
            {
                await OnStoppingAsync(cancellationToken);
                await OnStoppedAsync(cancellationToken);
            }

            catch (Exception e)
            {
                logger.LogCritical(e, $"{nameof(HostedService)}: {ServiceName}");
            }
        }

        #endregion

        #region Protected methods

        protected virtual Task OnStartingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnStartedAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{ServiceName} successfully started");

            return Task.CompletedTask;
        }

        protected virtual Task OnStoppingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnStoppedAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{ServiceName} successfully stopped");

            return Task.CompletedTask;
        }

        #endregion
    }
}

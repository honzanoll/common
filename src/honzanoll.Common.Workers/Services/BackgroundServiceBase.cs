using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.Common.Workers.Services
{
    public abstract class BackgroundServiceBase : BackgroundService
    {
        #region Fields

        private readonly ILogger<BackgroundService> logger;

        #endregion

        #region Protected members

        protected abstract string ServiceName { get; }

        #endregion

        #region Constructors

        public BackgroundServiceBase(ILogger<BackgroundService> logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public methods

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await OnStartingAsync(cancellationToken);
                await base.StartAsync(cancellationToken);
                await OnStartedAsync(cancellationToken);
            }

            catch (Exception e)
            {
                logger.LogCritical(e, $"{nameof(BackgroundServiceBase)}: {ServiceName}");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"{nameof(BackgroundServiceBase)}: {ServiceName} stopping...");

            try
            {
                await OnStoppingAsync(cancellationToken);
                await OnStoppedAsync(cancellationToken);
            }

            catch (Exception e)
            {
                logger.LogCritical(e, $"{nameof(BackgroundServiceBase)}: {ServiceName}");
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

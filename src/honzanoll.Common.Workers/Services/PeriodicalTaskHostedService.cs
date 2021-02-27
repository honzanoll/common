using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace honzanoll.Common.Workers.Services
{
    public abstract class PeriodicalTaskHostedService : BackgroundServiceBase
    {
        #region Fields

        private readonly CancellationTokenSource stoppingCancellationTokenSources = new CancellationTokenSource();

        private readonly List<Task> executingTasks = new List<Task>();

        #endregion

        #region Protected members

        protected abstract double DelaySecond { get; }

        #endregion

        #region Constructors

        public PeriodicalTaskHostedService(ILogger<PeriodicalTaskHostedService> logger) : base (logger) { }

        #endregion

        #region Protected methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Task executingTask = ExecutePeriodicalTaskAsync(stoppingCancellationTokenSources.Token);
                if (!executingTask.IsCompleted)
                    executingTasks.Add(executingTask);

                await Task.Delay(TimeSpan.FromSeconds(DelaySecond), stoppingToken);
            }
        }

        protected abstract Task ExecutePeriodicalTaskAsync(CancellationToken stoppingToken);

        protected override async Task OnStoppingAsync(CancellationToken cancellationToken)
        {
            if (executingTasks.Count == 0)
            {
                await base.OnStoppingAsync(cancellationToken);
                return;
            }

            try
            {
                stoppingCancellationTokenSources.Cancel();
            }
            finally
            {
                await Task.WhenAny(Task.WhenAll(executingTasks), Task.Delay(-1, cancellationToken));

                await base.OnStoppingAsync(cancellationToken);
            }
        }

        #endregion
    }
}

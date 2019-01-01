namespace TaxiChain.App
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class TaxiChainAppService : IHostedService, IDisposable
    {
        private readonly ILogger logger;

        public TaxiChainAppService(ILogger<TaxiChainAppService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("TaxiChain app started");

            return Task.CompletedTask;
        }

        public  Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("TaxiChain app stopped");

            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}

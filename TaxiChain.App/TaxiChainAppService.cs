namespace TaxiChain.App
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using TaxiChain.Services.Contracts;

    public class TaxiChainAppService : IHostedService, IDisposable
    {
        private readonly ILogger logger;

        private readonly ITaxiChainService taxiChainService;

        public TaxiChainAppService(ILogger<TaxiChainAppService> logger, ITaxiChainService taxiChainService)
        {
            this.logger = logger;
            this.taxiChainService = taxiChainService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("TaxiChain app started");

            await this.taxiChainService.OpenNetworkAsync();
            await this.ExecuteCommand();
        }

        public async  Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("TaxiChain app stopped");
        }

        private async Task ExecuteCommand()
        {
            while(true)
            {
                Console.WriteLine("Command ?");
                string command = Console.ReadLine();
                switch(command)
                {
                    case "startmine":
                        await this.taxiChainService.StartMineAsync(false);
                        Console.WriteLine("Mining started");
                        break;
                    case "startminegenesis":
                        await this.taxiChainService.StartMineAsync(true);
                        Console.WriteLine("Mining started");
                        break;
                    case "stopmine":
                        await this.taxiChainService.StopMineAsync();
                        Console.WriteLine("Mining stopped");
                        break;
                    default:
                        continue;
                }
            }
        }

        public void Dispose()
        {
            this.taxiChainService?.Dispose();
        }
    }
}

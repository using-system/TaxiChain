﻿namespace TaxiChain.App
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;

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

        public  Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("TaxiChain app stopped");

            return Task.CompletedTask;
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
                    case "list":
                        var requests = await this.taxiChainService.SearchCustomerRequestsAsync();
                        foreach (var request in requests)
                        {
                            Console.WriteLine($"Customer {Convert.ToBase64String(request.Customer)}, request {Convert.ToBase64String(request.RequestID)} is on Latitude {request.Position?.Latitude} and Longitude {request.Position?.Longitude}");
                        }
                        break;
                    case "request":
                        Console.WriteLine("Latitude ?");
                        double.TryParse(Console.ReadLine(), out double latitude);
                        Console.WriteLine("Logitude ?");
                        double.TryParse(Console.ReadLine(), out double longitude);
                        string transactionID = await this.taxiChainService.RequestDriverAsync(new Model.Position()
                        {
                            Latitude = latitude,
                            Longitude = longitude
                        });
                        Console.WriteLine($"Transaction {transactionID} sended !");
                        break;
                    case "accept":
                        Console.WriteLine("Customer ?");
                        string customerID = Console.ReadLine();
                        Console.WriteLine("Request ?");
                        string requestID = Console.ReadLine();
                        await this.taxiChainService.AcceptRequestAsync(Convert.FromBase64String(customerID), Convert.FromBase64String(requestID));
                        Console.WriteLine($"Transaction  sended !");
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

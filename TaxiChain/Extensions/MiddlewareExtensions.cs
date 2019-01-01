namespace TaxiChain.Extensions
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using NBlockchain.Models;

    using TaxiChain.Model;

    /// <summary>
    /// Middleware Extension methods
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the taxi chain.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="passphrase">The passphrase.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        public static IServiceCollection AddTaxiChain(this IServiceCollection services, 
            string passphrase, 
            uint port = 10850)
        {
            services.AddBlockchain(setup =>
            {
                setup.UseDataConnection("node.db");
                setup.UseInstructionRepository<Repositories.Contracts.ITaxiInstructionRepository, Repositories.TaxiInstructionLiteDbRepository>();
                setup.UseTcpPeerNetwork(port);
                setup.UseUpnpAutoNatTraversal("TaxiChain");
                //setup.UseMulticastDiscovery("TaxiChain", "224.100.0.1", 8088);
                setup.AddInstructionType<Transactions.RequestDriverInstruction>();

                setup.AddTransactionRule<Rules.AcceptExistingRequestRule>();

                setup.UseBlockbaseProvider<Transactions.TaxiChainTransactionBuilder>();
                setup.UseParameters(new StaticNetworkParameters()
                {
                    BlockTime = TimeSpan.FromSeconds(120),
                    HeaderVersion = 1
                });
            });

            services.AddSingleton<TaxiChainConfiguration>(provider => new TaxiChainConfiguration()
            {
                Passphrase = passphrase,
                Port = port
            });
            services.AddSingleton<Services.Contracts.ITaxiChainService, Services.TaxiChainService>();

            return services;
        }
    }
}

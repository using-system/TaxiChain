namespace TaxiChain.Extensions
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using NBlockchain.Models;

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
            services.AddBlockchain(x =>
            {
                x.UseDataConnection("node.db");
                x.UseInstructionRepository<Repositories.ITaxiInstructionRepository, Repositories.LiteDb.TaxiInstructionRepository>();
                x.UseTcpPeerNetwork(port);
                x.UseMulticastDiscovery("My Currency", "224.100.0.1", 8088);
                x.AddInstructionType<Transactions.RequestDriverInstruction>();
                x.UseParameters(new StaticNetworkParameters()
                {
                    BlockTime = TimeSpan.FromSeconds(120),
                    HeaderVersion = 1
                });
            });

            return services;
        }
    }
}

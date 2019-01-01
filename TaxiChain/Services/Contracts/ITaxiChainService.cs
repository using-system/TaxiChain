namespace TaxiChain.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Taxi Chain Service contract
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ITaxiChainService : IDisposable
    {
        /// <summary>
        /// Opens the network asynchronous.
        /// </summary>
        /// <returns></returns>
        Task OpenNetworkAsync();
    }
}

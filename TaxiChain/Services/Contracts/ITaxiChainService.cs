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

        /// <summary>
        /// Mines the asynchronous.
        /// </summary>
        /// <param name="genesis">if set to <c>true</c> [genesis].</param>
        /// <returns></returns>
        Task StartMineAsync(bool genesis);
        
        /// <summary>
        /// Stops the mine asynchronous.
        /// </summary>
        /// <returns></returns>
        Task StopMineAsync();
    }
}

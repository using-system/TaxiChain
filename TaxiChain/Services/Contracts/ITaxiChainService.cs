namespace TaxiChain.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaxiChain.Model;

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
        /// Requests the driver asynchronous.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <returns></returns>
        Task<string> RequestDriverAsync(Position startPosition);

        /// <summary>
        /// Searches the customers.
        /// </summary>
        /// <param name="nearBy">The near by.</param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> SearchCustomersAsync(Position nearBy = null);
        
        /// <summary>
        /// Stops the mine asynchronous.
        /// </summary>
        /// <returns></returns>
        Task StopMineAsync();
    }
}

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
        /// Searches the customer requests asynchronous.
        /// </summary>
        /// <param name="nearBy">The near by.</param>
        /// <returns></returns>
        Task<IEnumerable<CustomerRequest>> SearchCustomerRequestsAsync(Position nearBy = null);

        /// <summary>
        /// Requests the driver asynchronous.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <returns></returns>
        Task<string> RequestDriverAsync(Position startPosition);


        /// <summary>
        /// Accepts the request asynchronous.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="requestID">The request identifier.</param>
        /// <returns></returns>
        Task<string> AcceptRequestAsync(byte[] customer, byte[] requestID);
        
        /// <summary>
        /// Stops the mine asynchronous.
        /// </summary>
        /// <returns></returns>
        Task StopMineAsync();
    }
}

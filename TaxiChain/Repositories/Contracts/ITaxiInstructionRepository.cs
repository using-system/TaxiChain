namespace TaxiChain.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaxiChain.Model;

    /// <summary>
    /// Taxi Instruction Repository contract
    /// </summary>
    public interface ITaxiInstructionRepository
    {
        /// <summary>
        /// Searches the customers.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        Task<IEnumerable<CustomerRequest>> SearchCustomerRequestsAsync(Position position);

        /// <summary>
        /// Gets the customer request asynchronous.
        /// </summary>
        /// <param name="requestID">The request identifier.</param>
        /// <returns></returns>
        Task<CustomerRequest> GetCustomerRequestAsync(Byte[] requestID);
    }
}

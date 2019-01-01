namespace TaxiChain.Repositories.Contracts
{
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
        Task<IEnumerable<Customer>> SearchCustomersAsync(Position position);
    }
}

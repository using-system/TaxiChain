namespace TaxiChain.Repositories
{
    using System.Collections.Generic;

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
        IEnumerable<string> SearchCustomers(Position position);
    }
}

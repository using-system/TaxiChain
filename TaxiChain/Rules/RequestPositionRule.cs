namespace TaxiChain.Rules
{
    using System.Collections.Generic;
    using System.Linq;

    using NBlockchain.Interfaces;
    using NBlockchain.Models;

    /// <summary>
    /// Request Position Rule
    /// </summary>
    /// <seealso cref="NBlockchain.Interfaces.ITransactionRule" />
    public class RequestPositionRule : ITransactionRule
    {
        /// <summary>
        /// Validates the specified transaction.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="siblings">The siblings.</param>
        /// <returns></returns>
        public int Validate(Transaction transaction, ICollection<Transaction> siblings)
        {
            foreach (var instruction in transaction.Instructions.OfType<Transactions.RequestDriverInstruction>())
            {
                if (instruction.Start?.Latitude < -90
                    || instruction.Start?.Latitude > 90)
                {
                    return -1;
                }

                if (instruction.Start?.Longitude < -180
                    || instruction.Start?.Longitude > 180)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}

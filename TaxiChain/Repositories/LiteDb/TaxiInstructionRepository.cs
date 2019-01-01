namespace TaxiChain.Repositories.LiteDb
{
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    using NBlockchain.Interfaces;
    using NBlockchain.Services.Database;

    using TaxiChain.Model;

    /// <summary>
    /// Taxi Instruction Repository
    /// </summary>
    /// <seealso cref="NBlockchain.Services.Database.InstructionRepository" />
    /// <seealso cref="TaxiChain.Repositories.ITaxiInstructionRepository" />
    public class TaxiInstructionRepository : InstructionRepository, ITaxiInstructionRepository
    {
        private IAddressEncoder addressEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxiInstructionRepository" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="dataConnection">The data connection.</param>
        /// <param name="addressEncoder">The address encoder.</param>
        public TaxiInstructionRepository(ILoggerFactory loggerFactory, IDataConnection dataConnection, IAddressEncoder addressEncoder)
         : base(loggerFactory, dataConnection)
        {
            this.addressEncoder = addressEncoder;
        }

        /// <summary>
        /// Searches the customers.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public IEnumerable<string> SearchCustomers(Position position)
        {
            return this.Instructions
                .FindAll()
                .Select(x => x.Entity)
                .OfType<Transactions.RequestDriverInstruction>()
                .Select(instruction => this.addressEncoder.EncodeAddress(instruction.PublicKey, 0));
        }
    }
}

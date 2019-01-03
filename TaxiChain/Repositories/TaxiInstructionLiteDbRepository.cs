namespace TaxiChain.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;

    using NBlockchain.Interfaces;
    using NBlockchain.Services.Database;

    using LiteDB;

    using TaxiChain.Model;

    /// <summary>
    /// Taxi Instruction Repository
    /// </summary>
    /// <seealso cref="NBlockchain.Services.Database.InstructionRepository" />
    /// <seealso cref="TaxiChain.Repositories.ITaxiInstructionRepository" />
    public class TaxiInstructionLiteDbRepository : InstructionRepository, Contracts.ITaxiInstructionRepository
    {
        private IAddressEncoder addressEncoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxiInstructionLiteDbRepository" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="dataConnection">The data connection.</param>
        /// <param name="addressEncoder">The address encoder.</param>
        public TaxiInstructionLiteDbRepository(ILoggerFactory loggerFactory, IDataConnection dataConnection,
            IAddressEncoder addressEncoder)
            : base(loggerFactory, dataConnection)
        {
            this.addressEncoder = addressEncoder;
        }

        /// <summary>
        /// Searches the customers.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public Task<IEnumerable<CustomerRequest>> SearchCustomerRequestsAsync(Position position)
        {
            var customers = this.Instructions
                .FindAll()
                .Select(x => x.Entity)
                .OfType<Transactions.RequestDriverInstruction>()
                .Select(instruction => new CustomerRequest()
                {
                    Customer = instruction.PublicKey,
                    Position = instruction.Start,
                    RequestID = instruction.InstructionId
                });

            return Task.FromResult(customers);
        }

        /// <summary>
        /// Gets the customer request asynchronous.
        /// </summary>
        /// <param name="requestID">The request identifier.</param>
        /// <returns></returns>
        public Task<CustomerRequest> GetCustomerRequestAsync(Byte[] requestID)
        {
            var request = this.Instructions
                .Find(entity => entity.Entity.InstructionId == requestID)
                .Select(x => x.Entity)
                .OfType<Transactions.RequestDriverInstruction>()
                .Select(instruction => new CustomerRequest()
                {
                    Customer = instruction.PublicKey,
                    Position = instruction.Start,
                    RequestID = instruction.InstructionId
                })
                .SingleOrDefault();

            return Task.FromResult(request);
        }

        /// <summary>
        /// Gets the number of requests.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        public Task<int> GetNumberOfDriverRequestsAsync(Byte[] customer)
        {
            var count = this.Instructions
                .Find(entity => entity.Statistics.PublicKeyHash == customer)
                .OfType<Transactions.RequestDriverInstruction>()
                .Count();

            return Task.FromResult(count);
        }

        /// <summary>
        /// Gets the number of accepted requests.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        public Task<int> GetNumberOfAcceptedRequestsAsync(Byte[] customer)
        {
            var count = this.Instructions
                .Find(Query.EQ("Entity.Customer", customer))
                .OfType<Transactions.AcceptReqestInstruction>()             
                .Count();

            return Task.FromResult(count);
        }
    }
}

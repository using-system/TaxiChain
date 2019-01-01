namespace TaxiChain.Rules
{
    using System.Collections.Generic;
    using System.Linq;

    using NBlockchain.Interfaces;
    using NBlockchain.Models;

    using TaxiChain.Repositories.Contracts;


    public class AcceptExistingRequestRule : ITransactionRule
    {
        private ITaxiInstructionRepository taxiInstructionRepository;

        private IAddressEncoder addressEncoder;

        public AcceptExistingRequestRule(ITaxiInstructionRepository taxiInstructionRepository, IAddressEncoder addressEncoder)
        {
            this.taxiInstructionRepository = taxiInstructionRepository;
            this.addressEncoder = addressEncoder;
        }

        public int Validate(Transaction transaction, ICollection<Transaction> siblings)
        {
            var customers = this.taxiInstructionRepository.SearchCustomersAsync(null).Result;
            foreach (var instruction in transaction.Instructions.OfType<Transactions.AcceptReqestInstruction>())
            {
                if(!customers.Any(customer => customer.Address == instruction.CustomerAddress))
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}

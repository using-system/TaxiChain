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
            var requests = this.taxiInstructionRepository.SearchCustomerRequestsAsync(null).Result;
            foreach (var instruction in transaction.Instructions.OfType<Transactions.AcceptReqestInstruction>())
            {
                if(!requests.Any(request => request.RequestID == instruction.RequestID))
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}

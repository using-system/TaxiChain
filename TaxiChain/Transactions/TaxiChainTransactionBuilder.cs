namespace TaxiChain.Transactions
{
    using System.Collections.Generic;

    using NBlockchain.Interfaces;
    using NBlockchain.Models;
    using NBlockchain.Services;

    /// <summary>
    /// TaxiChainTransactionBuilder
    /// </summary>
    /// <seealso cref="NBlockchain.Services.BlockbaseTransactionBuilder" />
    public class TaxiChainTransactionBuilder : BlockbaseTransactionBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxiChainTransactionBuilder"/> class.
        /// </summary>
        /// <param name="addressEncoder">The address encoder.</param>
        /// <param name="signatureService">The signature service.</param>
        /// <param name="transactionBuilder">The transaction builder.</param>
        public TaxiChainTransactionBuilder(IAddressEncoder addressEncoder, ISignatureService signatureService, ITransactionBuilder transactionBuilder)
          : base(addressEncoder, signatureService, transactionBuilder)
        {
        }

        /// <summary>
        /// Builds the instructions.
        /// </summary>
        /// <param name="builderKeys">The builder keys.</param>
        /// <param name="transactions">The transactions.</param>
        /// <returns></returns>
        protected override ICollection<Instruction> BuildInstructions(KeyPair builderKeys, ICollection<Transaction> transactions)
        {
            var result = new List<Instruction>();
            var instruction = new RequestDriverInstruction
            {
                PublicKey = builderKeys.PublicKey
            };

            SignatureService.SignInstruction(instruction, builderKeys.PrivateKey);
            result.Add(instruction);

            return result;
        }
    }
}

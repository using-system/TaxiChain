namespace TaxiChain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NBlockchain.Interfaces;
    using NBlockchain.Models;

    using TaxiChain.Model;
    using Repositories.Contracts;

    /// <summary>
    /// Taxi Chain Service
    /// </summary>
    /// <seealso cref="TaxiChain.Services.Contracts.ITaxiChainService" />
    public class TaxiChainService : Contracts.ITaxiChainService
    {
        private KeyPair keys;

        private IPeerNetwork network;

        private ISignatureService signatureService;

        private IAddressEncoder addressEncoder;

        private IBlockMiner blockMiner;

        private ITransactionBuilder transactionBuilder;

        private IBlockchainNode blockchainNode;

        private ITaxiInstructionRepository taxiInstructionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxiChainService" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="network">The network.</param>
        /// <param name="signatureService">The signature service.</param>
        /// <param name="addressEncoder">The address encoder.</param>
        /// <param name="blockMiner">The block miner.</param>
        /// <param name="transactionBuilder">The transaction builder.</param>
        public TaxiChainService(TaxiChainConfiguration configuration,
            IPeerNetwork network,
            ISignatureService signatureService,
            IAddressEncoder addressEncoder,
            IBlockMiner blockMiner,
            ITransactionBuilder transactionBuilder,
            IBlockchainNode blockchainNode,
            ITaxiInstructionRepository taxiInstructionRepository)
        {
            this.network = network;
            this.signatureService = signatureService;
            this.addressEncoder = addressEncoder;
            this.blockMiner = blockMiner;
            this.transactionBuilder = transactionBuilder;
            this.blockchainNode = blockchainNode;
            this.taxiInstructionRepository = taxiInstructionRepository;

            this.keys = this.signatureService.GetKeyPairFromPhrase(configuration.Passphrase);
        }

        /// <summary>
        /// Opens the network asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task OpenNetworkAsync()
        {
            this.network.Open();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts the mine asynchronous.
        /// </summary>
        /// <param name="genesis">if set to <c>true</c> [genesis].</param>
        /// <returns></returns>
        public Task StartMineAsync(bool genesis)
        {
            this.blockMiner.Start(this.keys, genesis);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Searches the customers.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(Position nearBy = null)
        {
            return await this.taxiInstructionRepository.SearchCustomersAsync(nearBy);
        }

        /// <summary>
        /// Requests the driver asynchronous.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<string> RequestDriverAsync(Position startPosition)
        {
            var instruction = new Transactions.RequestDriverInstruction()
            {
                Start = startPosition,
                PublicKey = this.keys.PublicKey
            };

            this.signatureService.SignInstruction(instruction, this.keys.PrivateKey);
            var transaction = await this.transactionBuilder.Build(new List<Instruction>() { instruction });
            await this.blockchainNode.SendTransaction(transaction);

            return BitConverter.ToString(transaction.TransactionId);
        }

        /// <summary>
        /// Accepts the request.
        /// </summary>
        /// <param name="customerAddress">The customer address.</param>
        /// <returns></returns>
        public async Task<string> AcceptRequestAsync(string customerAddress)
        {
            var instruction = new Transactions.AcceptReqestInstruction()
            {
                CustomerAddress = customerAddress,
                PublicKey = this.keys.PublicKey
            };

            this.signatureService.SignInstruction(instruction, this.keys.PrivateKey);
            var transaction = await this.transactionBuilder.Build(new List<Instruction>() { instruction });
            await this.blockchainNode.SendTransaction(transaction);

            return BitConverter.ToString(transaction.TransactionId);
        }

        /// <summary>
        /// Stops the mine asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task StopMineAsync()
        {
            this.blockMiner.Stop();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.network?.Close();
            this.network = null;
        }


    }
}

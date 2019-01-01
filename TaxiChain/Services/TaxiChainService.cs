namespace TaxiChain.Services
{
    using System.Threading.Tasks;

    using NBlockchain.Interfaces;
    using NBlockchain.Models;

    using TaxiChain.Model;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxiChainService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="network">The network.</param>
        /// <param name="signatureService">The signature service.</param>
        /// <param name="addressEncoder">The address encoder.</param>
        public TaxiChainService(TaxiChainConfiguration configuration, 
            IPeerNetwork network,
            ISignatureService signatureService,
            IAddressEncoder addressEncoder,
            IBlockMiner blockMiner)
        {
            this.network = network;
            this.signatureService = signatureService;
            this.addressEncoder = addressEncoder;
            this.blockMiner = blockMiner;

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

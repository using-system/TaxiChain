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
            IAddressEncoder addressEncoder)
        {
            this.network = network;
            this.signatureService = signatureService;
            this.addressEncoder = addressEncoder;

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
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.network?.Close();
            this.network = null;
        }


    }
}

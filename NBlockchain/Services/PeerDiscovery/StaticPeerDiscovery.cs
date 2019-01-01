using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBlockchain.Interfaces;
using NBlockchain.Models;

namespace NBlockchain.Services.PeerDiscovery
{
    public class StaticPeerDiscovery : IPeerDiscoveryService
    {
        private readonly ICollection<string> _peers = new HashSet<string>();
        
        public StaticPeerDiscovery(ICollection<string> peers)
        {
            foreach (var item in peers)
                _peers.Add(item);
        }

        public async Task AdvertiseGlobal(string connectionString)
        {            
        }

        public async Task AdvertiseLocal(string connectionString)
        {
        }

        public async Task<ICollection<KnownPeer>> DiscoverPeers()
        {
            var result = _peers.Select(x => new KnownPeer() {ConnectionString = x}).ToList();
            return result;
        }

        public async Task SharePeers(ICollection<KnownPeer> peers)
        {
            await Task.Yield();
        }
    }
}

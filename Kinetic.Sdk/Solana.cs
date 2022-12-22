
using Microsoft.Extensions.Logging;
using Solnet.Rpc;


// ReSharper disable once CheckNamespace

namespace Kinetic.Sdk
{
    public class Solana
    {
        public readonly IRpcClient Connection;

        public Solana(string endpoint, ILogger? logger = null)
        {
            Connection = ClientFactory.GetClient(endpoint, logger);
        }
    }
}
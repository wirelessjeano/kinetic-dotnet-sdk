using Microsoft.Extensions.Logging;


// ReSharper disable once CheckNamespace


namespace Kinetic.Sdk.Interfaces
{
    public class KineticSdkConfig
    {
        public Commitment? Commitment;
        public readonly string Endpoint;
        public readonly string Environment;
        public readonly int Index;
        public readonly ILogger Logger;
        public string SolanaRpcEndpoint;

        public KineticSdkConfig(
            string endpoint,
            string environment,
            int index,
            ILogger logger = null,
            Commitment? commitment = null,
            string solanaRpcEndpoint = null
        )
        {
            Index = index;
            Endpoint = endpoint;
            Environment = environment;
            Commitment = commitment;
            Logger = logger;
            SolanaRpcEndpoint = solanaRpcEndpoint;
        }
    }
}
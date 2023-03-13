using Google.Protobuf;
using Graffle.FlowSdk.Cryptography;
using Graffle.FlowSdk.Types;
using Grpc.Core;
using Grpc.Net.Client;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graffle.FlowSdk
{
    public class FlowClient
    {
        /// <summary>
        /// GRPC channel options
        /// </summary>
        private static readonly GrpcChannelOptions GRPC_CHANNEL_OPTIONS = new GrpcChannelOptions()
        {
            Credentials = ChannelCredentials.Insecure,
            MaxReceiveMessageSize = null, //null = no limit
        };

        private readonly Flow.Access.AccessAPI.AccessAPIClient _client;

        public FlowClient(Flow.Access.AccessAPI.AccessAPIClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _client = client;
        }

        /// <summary>
        /// Create a new instance of the Flow client.
        /// </summary>
        /// <param name="accessNodeURI">Access node URL and port. Ex: access.testnet.nodes.onflow.org:9000 for testnet.</param>
        /// <returns></returns>
        public static FlowClient Create(string accessNodeURI)
        {
            if (!accessNodeURI.StartsWith("http://"))
                accessNodeURI = $"http://{accessNodeURI}";

            var channel = GrpcChannel.ForAddress(accessNodeURI, GRPC_CHANNEL_OPTIONS);
            var client = new Flow.Access.AccessAPI.AccessAPIClient(channel);
            var flowClient = new FlowClient(client);
            return flowClient;
        }

        public static FlowClient Create(GrpcChannel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));

            var client = new Flow.Access.AccessAPI.AccessAPIClient(channel);
            return new FlowClient(client);
        }

        public async Task<bool> Ping()
        {
            var res = await _client.PingAsync(new Flow.Access.PingRequest());
            return res != null;
        }

        public async Task<Flow.Access.BlockResponse> GetLatestBlockAsync(bool isSealed = true, CallOptions options = new CallOptions())
        {
            var request = new Flow.Access.GetLatestBlockRequest() { IsSealed = isSealed };
            var result = await _client.GetLatestBlockAsync(request, options);
            return result;
        }

        public async Task<Flow.Access.EventsResponse> GetEventsForHeightRangeAsync(string eventType, ulong startHeight, ulong endHeight, CallOptions options = new CallOptions())
        {
            var request = new Flow.Access.GetEventsForHeightRangeRequest() { Type = eventType, StartHeight = startHeight, EndHeight = endHeight };
            var result = await _client.GetEventsForHeightRangeAsync(request, options);
            return result;
        }

        public async Task<Flow.Access.ExecuteScriptResponse> ExecuteScriptAtBlockHeightAsync(ulong blockHeight, byte[] cadenceScript, IEnumerable<FlowValueType> args, CallOptions options = new CallOptions())
        {
            var scriptByteString = ByteString.CopyFrom(cadenceScript);
            var request = new Flow.Access.ExecuteScriptAtBlockHeightRequest() { BlockHeight = blockHeight, Script = scriptByteString };
            foreach (var arg in args)
                request.Arguments.Add(arg.ToByteString());

            return await _client.ExecuteScriptAtBlockHeightAsync(request, options);
        }

        public async Task<Flow.Access.ExecuteScriptResponse> ExecuteScriptAtBlockIdAsync(ByteString blockId, byte[] cadenceScript, IEnumerable<FlowValueType> args, CallOptions options = new CallOptions())
        {
            var scriptByteString = ByteString.CopyFrom(cadenceScript);
            var request = new Flow.Access.ExecuteScriptAtBlockIDRequest() { BlockId = blockId, Script = scriptByteString };
            foreach (var arg in args)
                request.Arguments.Add(arg.ToByteString());

            return await _client.ExecuteScriptAtBlockIDAsync(request, options);
        }

        public async Task<Flow.Access.ExecuteScriptResponse> ExecuteScriptAtLatestBlockAsync(byte[] script, List<FlowValueType> args, CallOptions options = new CallOptions())
        {
            var scriptByteString = ByteString.CopyFrom(script);
            var request = new Flow.Access.ExecuteScriptAtLatestBlockRequest() { Script = scriptByteString };

            foreach (var arg in args)
                request.Arguments.Add(arg.ToByteString());

            return await _client.ExecuteScriptAtLatestBlockAsync(request, options);
        }

        public async Task<Flow.Access.BlockResponse> GetBlockByHeightAsync(ulong blockHeight, CallOptions options = new CallOptions())
        {
            var request = new Flow.Access.GetBlockByHeightRequest() { Height = blockHeight };
            return await _client.GetBlockByHeightAsync(request, options);
        }

        public async Task<Flow.Access.TransactionResponse> GetTransactionAsync(ByteString transactionId)
        {
            var result = await _client.GetTransactionAsync(new Flow.Access.GetTransactionRequest() { Id = transactionId });
            return result;
        }

        public async Task<Flow.Access.AccountResponse> GetAccountAsync(string address, ulong blockHeight)
        {
            var result = await _client.GetAccountAtBlockHeightAsync(
                new Flow.Access.GetAccountAtBlockHeightRequest()
                {
                    BlockHeight = blockHeight,
                    Address = address.HexToByteString()
                });
            return result;
        }

        public async Task<Flow.Access.CollectionResponse> GetCollectionById(ByteString collectionId)
        {
            var request = new Flow.Access.GetCollectionByIDRequest() { Id = collectionId };
            var result = await _client.GetCollectionByIDAsync(request);
            return result;
        }

        public async Task<Flow.Access.TransactionResultResponse> GetTransactionResult(ByteString transactionId)
        {
            var request = new Flow.Access.GetTransactionRequest() { Id = transactionId };
            var result = await _client.GetTransactionResultAsync(request);
            return result;
        }

        public async Task<Flow.Entities.Account> GetAccountAtLatestBlockAsync(ByteString address)
        {
            var request = new Flow.Access.GetAccountAtLatestBlockRequest()
            {
                Address = address
            };

            var accountReponse = await _client.GetAccountAtLatestBlockAsync(request);

            return accountReponse.Account;
        }

        public async Task<Flow.Entities.Account> GetAccountFromConfigAsync(string name, string filePath = null)
        {
            var flowJsonConfig = Helpers.LoadFlowJson(filePath);

            if (!flowJsonConfig.Accounts.ContainsKey(name))
            {
                throw new Exception($"Could not load account '{name}' from flow.json");
            }

            var flowJsonAccount = flowJsonConfig.Accounts[name];

            var account = await GetAccountAtLatestBlockAsync(flowJsonAccount.Address.HexToByteString());

            if (!string.IsNullOrEmpty(flowJsonAccount.Key))
            {
                foreach (var key in account.Keys)
                {
                    var keyPair = ECDSA.AsymmetricCipherKeyPairFromPrivateKey(flowJsonAccount.Key, (SignatureAlgorithm)key.SignAlgo);
                    var publicKey = ECDSA.PublicKeyToHex(keyPair);

                    var flowAccountKey = account.Keys.Where(x => x.PublicKey.ByteStringToHex() == publicKey).FirstOrDefault();

                    if (flowAccountKey != null)
                    {
                        flowAccountKey.PrivateKey = flowJsonAccount.Key;

                        var privateKey = keyPair.Private as ECPrivateKeyParameters;
                        flowAccountKey.Signer = new ECDSAMessageSigner(privateKey, (HashAlgorithm)flowAccountKey.HashAlgo, (SignatureAlgorithm)flowAccountKey.SignAlgo);
                    }
                }
            }

            return account;
        }

        public async Task<Flow.Access.SendTransactionResponse> SendTransactionAsync(Flow.Access.SendTransactionRequest sendTransactionRequest, CallOptions options = new CallOptions())
        {
            return await _client.SendTransactionAsync(sendTransactionRequest, options);
        }
    }
}
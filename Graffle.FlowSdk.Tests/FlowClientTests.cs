using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Graffle.FlowSdk.Types;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests
{
    [TestClass]
    public class FlowClientTests
    {
        private static GrpcChannel _mainNet;
        private static JsonSerializerOptions _jsonOptions;

        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            _mainNet = GrpcChannel.ForAddress("http://access.mainnet.nodes.onflow.org:9000", new GrpcChannelOptions() { Credentials = ChannelCredentials.Insecure });
            _jsonOptions = new();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _mainNet?.Dispose();
        }

        [TestMethod]
        public async Task Ping()
        {
            var client = FlowClient.Create(_mainNet);
            Assert.IsTrue(await client.Ping());
        }

        [TestMethod]
        public async Task GetLatestBlockAsync()
        {
            var client = FlowClient.Create(_mainNet);

            var block = await client.GetLatestBlockAsync(true);
            Assert.IsTrue(block.Block.Height > 0ul);
        }

        [TestMethod]
        public async Task ExecuteScriptAtLatestBlock()
        {
            const string script = @"pub fun main(arg: String): String { return arg }";
            var arg = new StringType("foo");

            var bytes = Encoding.UTF8.GetBytes(script);

            var client = FlowClient.Create(_mainNet);
            var res = await client.ExecuteScriptAtLatestBlockAsync(bytes, new List<FlowValueType>() { arg });
            string responseJson = Encoding.UTF8.GetString(res.Value.ToByteArray());

            var parsedResult = FlowValueType.CreateFromCadence(responseJson.Replace("\n", ""));

            Assert.IsInstanceOfType(parsedResult, typeof(StringType));
            var str = parsedResult as StringType;
            Assert.AreEqual("foo", str.Data);
        }

        // [TestMethod]
        // public async Task GetEventsForHeightRangeAsync()
        // {
        //     var client = FlowClient.Create(_mainNet);
        //     var res = await client.GetEventsForHeightRangeAsync("A.1654653399040a61.FlowToken.TokensDeposited", 48359903, 48359903);
        // }
    }
}
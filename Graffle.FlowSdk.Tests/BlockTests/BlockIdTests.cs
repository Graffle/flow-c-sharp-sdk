using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graffle.FlowSdk.Tests.BlockTests
{
    [TestClass]
    [Ignore]
    public class BlockIdTests
    {
        private FlowClient flowClient { get; }

        public BlockIdTests()
        {
            this.flowClient = FlowClient.Create(TestHelpers.EmulatorURI);
        }

        [TestMethod]
        public async Task Given_HelloWorld_When_ExecuteScriptAtBlockIdAsync_Then_Return_Successful_Result()
        {
            var latestBlockResponse = await this.flowClient.GetLatestBlockAsync(true);

            var helloWorldScript = @"
                pub fun main(): String {
                    return ""Hello World""
                }
            ";

            var scriptBytes = Encoding.ASCII.GetBytes(helloWorldScript);

            var scriptResponse = await flowClient.ExecuteScriptAtBlockIdAsync(latestBlockResponse.Block.Id, scriptBytes, new List<FlowValueType>());
            var metaDataJson = Encoding.Default.GetString(scriptResponse.Value.ToByteArray());
            var result = StringType.FromJson(metaDataJson);

            Assert.AreEqual(result.Data, "Hello World");
            Assert.AreEqual(result.Type, "String");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.Json;

namespace Graffle.FlowSdk.Tests.AccountTests
{
    [TestClass]
    [Ignore]
    public class ConfigAccountTests
    {
        private FlowClient flowClient { get; }

        public ConfigAccountTests()
        {
            this.flowClient = FlowClient.Create(TestHelpers.EmulatorURI);
        }

        [TestMethod]
        public async Task Given_Valid_AccountName_When_GetAccountFromConfigAsync_Then_Load_And_Parse_From_FlowJson()
        {
            var account = await flowClient.GetAccountFromConfigAsync("emulator-account");
            Assert.AreEqual("f8d6e0586b0a20c7", account.Address.ByteStringToHex());
            Assert.AreEqual((UInt32)2, account.Keys[0].SignAlgo);
            Assert.AreEqual((UInt32)3, account.Keys[0].HashAlgo);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.Json;

namespace Graffle.FlowSdk.Tests.HelpersTests
{
    [TestClass]
    public class FlowJsonTests
    {
        private FlowClient flowClient { get; }
        
        public FlowJsonTests(){
            this.flowClient = FlowClient.Create(TestHelpers.EmulatorURI);
        }        

        [TestMethod]
        public void Given_Valid_FlowJson_File_Then_Parse_And_Return(){
            var flowConfig = Helpers.LoadFlowJson();
            var emulatorAccount = flowConfig.Accounts["emulator-account"];
            Assert.AreEqual("f8d6e0586b0a20c7", emulatorAccount.Address);
            Assert.AreEqual("17011acae35618eb2a9c1670830cf01fb9b6160cf2c63a11d45e5c57089f3574", emulatorAccount.Key);
        }
    }
}
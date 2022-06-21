using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class RepeatedTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var type = "0x3.GreatContract.NFT";
            var rep = new RepeatedTypeDefinition(type);

            var json = rep.AsJsonCadenceDataFormat();

            Assert.AreEqual($"\"{type}\"", json);
        }

        [TestMethod]
        public void Flatten_ReturnsTypeString()
        {
            var type = "0x3.GreatContract.NFT";
            var rep = new RepeatedTypeDefinition(type);

            var res = rep.Flatten();
            Assert.AreEqual(type, res);
        }
    }
}
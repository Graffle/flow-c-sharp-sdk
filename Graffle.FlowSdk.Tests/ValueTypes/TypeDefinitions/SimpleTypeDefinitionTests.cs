using Graffle.FlowSdk.Types.StructuredTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class SimpleTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedJson = "{\"kind\":\"UInt8\"}";

            var type = new SimpleTypeDefinition("UInt8");

            var json = type.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var type = new SimpleTypeDefinition("UInt8");

            var result = type.Flatten();

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result.ContainsKey("kind"));

            var kind = result["kind"];

            Assert.AreEqual(kind, "UInt8");
        }
    }
}
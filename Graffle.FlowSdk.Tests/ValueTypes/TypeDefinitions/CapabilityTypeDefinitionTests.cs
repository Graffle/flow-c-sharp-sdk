using Graffle.FlowSdk.Types.StructuredTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class CapabilityTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedJson = "{\"kind\":\"Capability\",\"type\":{\"kind\":\"UInt8\"}}";

            var simpleType = new SimpleTypeDefinition("UInt8");

            var capabilityType = new CapabilityTypeDefinition("Capability", simpleType);

            var json = capabilityType.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simpleType = new SimpleTypeDefinition("UInt8");

            var capabilityType = new CapabilityTypeDefinition("Capability", simpleType);

            var result = capabilityType.Flatten();

            Assert.IsTrue(result.ContainsKey("kind"));
            Assert.AreEqual(result["kind"], "Capability");

            Assert.IsTrue(result.ContainsKey("type"));

            var innerDictionary = result["type"];

            Assert.IsTrue(innerDictionary.ContainsKey("kind"));
            Assert.AreEqual(innerDictionary["kind"], "UInt8");
        }

        [TestMethod]
        public void Flatten_EmptyType_ReturnsDictionaryWithEmptyType()
        {
            var capabilityType = new CapabilityTypeDefinition("Capability", null);

            var result = capabilityType.Flatten();

            Assert.IsTrue(result.ContainsKey("kind"));
            Assert.AreEqual(result["kind"], "Capability");

            Assert.IsTrue(result.ContainsKey("type"));
            Assert.AreEqual(result["type"], string.Empty);
        }
    }
}
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class FieldTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedJson = "{\"id\":\"foo\",\"type\":{\"kind\":\"String\"}}";

            var simpleType = new SimpleTypeDefinition("String");

            var FiledType = new FieldTypeDefinition("foo", simpleType);

            var json = FiledType.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simpleType = new SimpleTypeDefinition("String");

            var capabilityType = new FieldTypeDefinition("foo", simpleType);

            var result = capabilityType.Flatten();

            Assert.IsTrue(result.ContainsKey("type"));
            Assert.AreEqual(result["type"]["kind"], "String");

            Assert.IsTrue(result.ContainsKey("id"));
            Assert.AreEqual(result["id"], "foo");
        }
    }
}
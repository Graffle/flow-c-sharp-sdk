using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class DictionaryTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedCadenceJson = "{\"kind\":\"Dictionary\",\"key\":{\"kind\":\"String\"},\"value\":{\"kind\":\"UInt8\"}}";

            var key = new SimpleTypeDefinition("String");

            var value = new SimpleTypeDefinition("UInt8");

            var dict = new DictionaryTypeDefinition(key, value);

            var json = dict.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedCadenceJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var key = new SimpleTypeDefinition("String");

            var value = new SimpleTypeDefinition("UInt8");

            var dict = new DictionaryTypeDefinition(key, value);

            var result = dict.Flatten();

            Assert.IsTrue(result.ContainsKey("kind"));
            Assert.AreEqual(result["kind"], dict.Kind);

            Assert.IsTrue(result.ContainsKey("key"));
            var keyRes = result["key"];
            Assert.IsInstanceOfType(keyRes, typeof(Dictionary<string, dynamic>));

            var realKey = result["key"] as Dictionary<string, dynamic>;
            Assert.AreEqual("String", realKey["kind"]);

            Assert.IsTrue(result.ContainsKey("value"));
            var valueRes = result["value"];
            Assert.IsInstanceOfType(valueRes, typeof(Dictionary<string, object>));

            var realValue = valueRes as Dictionary<string, object>;
            Assert.AreEqual("UInt8", realValue["kind"]);
        }
    }
}
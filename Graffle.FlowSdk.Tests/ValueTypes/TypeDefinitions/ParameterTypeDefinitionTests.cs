using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class ParameterTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedCadenceJson = "{\"label\":\"testLabel\",\"id\":\"123\",\"type\":{\"kind\":\"String\"}}";

            var simple = new SimpleTypeDefinition("String");

            var initializer = new ParameterTypeDefinition("testLabel", "123", simple);

            var json = initializer.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedCadenceJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simple = new SimpleTypeDefinition("String");

            var initializer = new ParameterTypeDefinition("testLabel", "123", simple);

            var result = initializer.Flatten();

            Assert.AreEqual("testLabel", result["label"]);

            Assert.AreEqual("123", result["id"]);

            var type = result["type"];

            var typeDict = type as Dictionary<string, object>;

            Assert.AreEqual("String", typeDict["kind"]);
        }
    }
}
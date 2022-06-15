using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class ReferenceTypeTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedCadenceJson = "{\"kind\":\"Reference\",\"authorized\":True,\"type\":{\"kind\":\"String\"}}";

            var simple = new SimpleTypeDefinition("String");

            var reference = new ReferenceTypeDefinition(true, simple);

            var json = reference.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedCadenceJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simple = new SimpleTypeDefinition("String");

            var reference = new ReferenceTypeDefinition(true, simple);

            var result = reference.Flatten();

            Assert.AreEqual("Reference", result["kind"]);

            Assert.AreEqual(true, result["authorized"]);

            var type = result["type"];

            var typeDict = type as Dictionary<string, object>;

            Assert.AreEqual("String", typeDict["kind"]);
        }
    }
}
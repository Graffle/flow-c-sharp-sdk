using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class VariableSizedArrayTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var simple = new SimpleTypeDefinition("UInt8");
            var arr = new VariableSizedArrayTypeDefinition(simple);

            var json = arr.AsJsonCadenceDataFormat();

            var expectedJson = "{\"kind\":\"VariableSizedArray\",\"type\":{\"kind\":\"UInt8\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simple = new SimpleTypeDefinition("UInt8");
            var arr = new VariableSizedArrayTypeDefinition(simple);

            var res = arr.Flatten();
            Assert.AreEqual("VariableSizedArray", res["kind"]);

            var resType = res["type"];
            Assert.IsInstanceOfType(resType, typeof(Dictionary<string, object>));

            var resTypeDict = resType as Dictionary<string, object>;
            Assert.AreEqual("UInt8", resTypeDict["kind"]);
        }
    }
}
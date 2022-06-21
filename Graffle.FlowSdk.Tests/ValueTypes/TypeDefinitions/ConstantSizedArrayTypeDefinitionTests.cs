using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class ConstantSizedArrayTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var simple = new SimpleTypeDefinition("UInt8");
            var arr = new ConstantSizedArrayTypeDefinition(simple, 500);

            var json = arr.AsJsonCadenceDataFormat();

            var expectedJson = "{\"kind\":\"ConstantSizedArray\",\"size\":500,\"type\":{\"kind\":\"UInt8\"}}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simple = new SimpleTypeDefinition("UInt8");
            var arr = new ConstantSizedArrayTypeDefinition(simple, 500);

            var res = arr.Flatten();
            Assert.AreEqual("ConstantSizedArray", res["kind"]);
            Assert.AreEqual((ulong)500, res["size"]);

            var resType = res["type"];
            Assert.IsInstanceOfType(resType, typeof(Dictionary<string, object>));

            var resTypeDict = resType as Dictionary<string, object>;
            Assert.AreEqual("UInt8", resTypeDict["kind"]);
        }
    }
}
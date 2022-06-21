using System.Collections.Generic;
using System.Linq;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class FunctionTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var expectedJson = "{\"kind\":\"Function\",\"typeID\":\"typeIDA\",\"parameters\":[{\"label\":\"pl\",\"id\":\"pid\",\"type\":{\"kind\":\"String\"}}],\"return\":{\"kind\":\"UInt\"}}";

            var simple1 = new SimpleTypeDefinition("String");
            var simple2 = new SimpleTypeDefinition("UInt");

            var param = new ParameterTypeDefinition("pl", "pid", simple1);
            var parameters = new List<ParameterTypeDefinition>() { param };

            var function = new FunctionTypeDefinition("typeIDA", parameters, simple2);

            var json = function.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_HasAllProperties()
        {
            var simple1 = new SimpleTypeDefinition("String");
            var simple2 = new SimpleTypeDefinition("UInt");

            var param = new ParameterTypeDefinition("pl", "pid", simple1);
            var parameters = new List<ParameterTypeDefinition>() { param };

            var function = new FunctionTypeDefinition("typeIDA", parameters, simple2);

            var res = function.Flatten();
            Assert.IsInstanceOfType(res, typeof(Dictionary<string, object>));
            var resDict = res as Dictionary<string, object>;

            Assert.AreEqual("Function", res["kind"]);
            Assert.AreEqual("typeIDA", res["typeID"]);

            var resReturn = resDict["return"];
            Assert.IsInstanceOfType(resReturn, typeof(Dictionary<string, object>));
            var resReturnDict = resReturn as Dictionary<string, object>;
            Assert.AreEqual("UInt", resReturnDict["kind"]);

            var resParameters = resDict["parameters"];
            Assert.IsInstanceOfType(resParameters, typeof(List<object>));
            var resParametersList = resParameters as List<object>;

            Assert.AreEqual(1, resParametersList.Count);
            var resParameter = resParametersList.First();
            Assert.IsInstanceOfType(resParameter, typeof(Dictionary<string, object>));

            var resParameterDict = resParameter as Dictionary<string, object>;
            Assert.AreEqual("pl", resParameterDict["label"]);
            Assert.AreEqual("pid", resParameterDict["id"]);

            var resParameterType = resParameterDict["type"];
            Assert.IsInstanceOfType(resParameterType, typeof(Dictionary<string, object>));

            var resParameterTypeDict = resParameterType as Dictionary<string, object>;
            Assert.AreEqual("String", resParameterTypeDict["kind"]);
        }
    }
}
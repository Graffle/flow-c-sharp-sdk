using System.Collections.Generic;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class InitializerTypeTests
    {
        [TestMethod]
        public void FromJson_ReturnsInitializerTypeWithAllParameters()
        {
            var json = "[{\"label\":\"l1\", \"id\":\"x1\", \"type\": {\"kind\":\"String\"}}, {\"label\":\"l2\", \"id\":\"x2\", \"type\": {\"kind\":\"UInt\"}}]";

            var init = InitializerTypeDefinition.FromJson(json);

            Assert.AreEqual(2, init.Parameters.Count);

            var param1 = init.Parameters[0];
            Assert.AreEqual("l1", param1.Label);
            Assert.AreEqual("x1", param1.Id);

            Assert.IsInstanceOfType(param1.Type, typeof(SimpleTypeDefinition));
            var param1Type = param1.Type as SimpleTypeDefinition;
            Assert.AreEqual("String", param1Type.Kind);

            var param2 = init.Parameters[1];
            Assert.AreEqual("l2", param2.Label);
            Assert.AreEqual("x2", param2.Id);

            Assert.IsInstanceOfType(param2.Type, typeof(SimpleTypeDefinition));
            var param2Type = param2.Type as SimpleTypeDefinition;
            Assert.AreEqual("UInt", param2Type.Kind);
        }

        [TestMethod]
        public void Flatten_HasAllProperties()
        {
            var json = "[{\"label\":\"l1\", \"id\":\"x1\", \"type\": {\"kind\":\"String\"}}, {\"label\":\"l2\", \"id\":\"x2\", \"type\": {\"kind\":\"UInt\"}}]";

            var init = InitializerTypeDefinition.FromJson(json);

            var res = init.Flatten();

            Assert.IsInstanceOfType(res, typeof(List<object>));
            var resList = res as List<object>;

            Assert.AreEqual(2, resList.Count);

            var first = resList[0];
            Assert.IsInstanceOfType(first, typeof(Dictionary<string, object>));
            var firstDict = first as Dictionary<string, object>;

            Assert.AreEqual("l1", firstDict["label"]);
            Assert.AreEqual("x1", firstDict["id"]);

            var firstType = firstDict["type"];
            Assert.IsInstanceOfType(firstType, typeof(Dictionary<string, object>));
            var firstTypeDict = firstType as Dictionary<string, object>;

            Assert.AreEqual("String", firstTypeDict["kind"]);

            var second = resList[1];
            Assert.IsInstanceOfType(second, typeof(Dictionary<string, object>));
            var secondDict = second as Dictionary<string, object>;

            Assert.AreEqual("l2", secondDict["label"]);
            Assert.AreEqual("x2", secondDict["id"]);

            var secondType = secondDict["type"];
            Assert.IsInstanceOfType(secondType, typeof(Dictionary<string, object>));
            var secondTypeDict = secondType as Dictionary<string, object>;

            Assert.AreEqual("UInt", secondTypeDict["kind"]);
        }

        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedJson = "[{\"label\":\"l1\",\"id\":\"x1\",\"type\":{\"kind\":\"String\"}},{\"label\":\"l2\",\"id\":\"x2\",\"type\":{\"kind\":\"UInt\"}}]";

            var simple1 = new SimpleTypeDefinition("String");
            var simple2 = new SimpleTypeDefinition("UInt");

            var param1 = new ParameterTypeDefinition("l1", "x1", simple1);
            var param2 = new ParameterTypeDefinition("l2", "x2", simple2);

            List<ParameterTypeDefinition> parameters = new List<ParameterTypeDefinition>() { param1, param2 };

            var init = new InitializerTypeDefinition(parameters);

            var json = init.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }
    }
}
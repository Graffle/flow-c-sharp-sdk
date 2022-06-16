using System.Collections.Generic;
using System.Linq;
using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class CompositeTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var expectedJson = "{\"kind\":\"Resource\",\"typeID\":\"compositeTypeId\",\"fields\":[{\"id\":\"fieldId\",\"type\":{\"kind\":\"Int32\"}}],\"initializers\":[],\"type\":\"\"}";

            var fieldType = new SimpleTypeDefinition("Int32");
            var field = new FieldTypeDefinition("fieldId", fieldType);

            var composite = new CompositeTypeDefinition("Resource", "compositeTypeId", new List<InitializerTypeDefinition>(), new List<FieldTypeDefinition>() { field });

            var json = composite.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_HasAllProperties()
        {
            var simple1 = new SimpleTypeDefinition("String");
            var simple2 = new SimpleTypeDefinition("UInt");

            var parameter = new ParameterTypeDefinition("pl", "pi", simple1);
            var initializer = new InitializerTypeDefinition(new List<ParameterTypeDefinition>() { parameter });
            var initializers = new List<InitializerTypeDefinition>() { initializer };

            var field = new FieldTypeDefinition("fi", simple2);
            var fields = new List<FieldTypeDefinition>() { field };

            var composite = new CompositeTypeDefinition("Resource", "typeID", initializers, fields);

            var res = composite.Flatten();

            Assert.AreEqual("Resource", res["kind"]);
            Assert.AreEqual(string.Empty, res["type"]);
            Assert.AreEqual("typeID", res["typeID"]);

            //verify initializers
            //initializers is an array of arrays sorry about this -_-
            var resultInitializers = res["initializers"];
            Assert.IsInstanceOfType(resultInitializers, typeof(List<object>));
            var resultInitializerList = resultInitializers as List<object>;
            Assert.AreEqual(1, resultInitializerList.Count);

            var resultInit = resultInitializerList.First();
            Assert.IsInstanceOfType(resultInit, typeof(List<object>));
            var resultInitList = resultInit as List<object>;
            Assert.AreEqual(1, resultInitList.Count);

            var resultParam = resultInitList.First();
            Assert.IsInstanceOfType(resultParam, typeof(Dictionary<string, object>));
            var resultParamDict = resultParam as Dictionary<string, object>;

            Assert.AreEqual("pl", resultParamDict["label"]);
            Assert.AreEqual("pi", resultParamDict["id"]);

            var paramType = resultParamDict["type"];
            Assert.IsInstanceOfType(paramType, typeof(Dictionary<string, object>));
            var paramTypeDict = paramType as Dictionary<string, object>;
            Assert.AreEqual("String", paramTypeDict["kind"]);

            //verify fields
            var resultFields = res["fields"];
            Assert.IsInstanceOfType(resultFields, typeof(List<object>));
            var resultFieldsList = resultFields as List<object>;
            Assert.AreEqual(1, resultFieldsList.Count);

            var resultField = resultFieldsList.First();
            Assert.IsInstanceOfType(resultField, typeof(Dictionary<string, object>));
            var resultFieldDict = resultField as Dictionary<string, object>;

            Assert.AreEqual("fi", resultFieldDict["id"]);

            var fieldType = resultFieldDict["type"];
            Assert.IsInstanceOfType(fieldType, typeof(Dictionary<string, object>));
            var fieldTypeDict = fieldType as Dictionary<string, object>;
            Assert.AreEqual("UInt", fieldTypeDict["kind"]);
        }
    }
}
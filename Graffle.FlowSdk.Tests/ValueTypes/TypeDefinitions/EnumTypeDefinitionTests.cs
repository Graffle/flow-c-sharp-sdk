using System.Collections.Generic;
using System.Linq;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class EnumTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectJson()
        {
            var expectedJson = "{\"kind\":\"Enum\",\"type\":{\"kind\":\"String\"},\"typeID\":\"testTypeId\",\"initializers\":[],\"fields\":[{\"id\":\"fieldId\",\"type\":{\"kind\":\"UInt8\"}}]}";

            var simpleStringType = new SimpleTypeDefinition("String");
            var uInt8Type = new SimpleTypeDefinition("UInt8");

            string typeId = "testTypeId";

            var field = new FieldTypeDefinition("fieldId", uInt8Type);
            var fields = new List<FieldTypeDefinition>() { field };

            var initializers = new List<InitializerTypeDefinition>();

            var enumType = new EnumTypeDefinition(simpleStringType, typeId, fields, initializers);

            var json = enumType.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var simpleStringType = new SimpleTypeDefinition("String");
            var uInt8Type = new SimpleTypeDefinition("UInt8");

            string typeId = "testTypeId";

            var field = new FieldTypeDefinition("fieldId", uInt8Type);
            var fields = new List<FieldTypeDefinition>() { field };

            var initializers = new List<InitializerTypeDefinition>();

            var enumType = new EnumTypeDefinition(simpleStringType, typeId, fields, initializers);

            var res = enumType.Flatten();

            Assert.AreEqual(typeId, res["typeID"]);

            var resultType = res["type"];
            Assert.IsInstanceOfType(resultType, typeof(Dictionary<string, object>));
            var typeDict = resultType as Dictionary<string, object>;
            Assert.AreEqual("String", typeDict["kind"]);

            var resultFields = res["fields"];
            Assert.IsInstanceOfType(resultFields, typeof(List<object>));
            var fieldsList = resultFields as List<object>;

            Assert.AreEqual(1, fieldsList.Count);
            var resultField = fieldsList.First() as Dictionary<string, object>;
            Assert.IsNotNull(resultField);
            Assert.AreEqual(field.Id, resultField["id"]);

            var fieldType = resultField["type"];
            Assert.IsInstanceOfType(fieldType, typeof(Dictionary<string, object>));

            var fieldDict = fieldType as Dictionary<string, object>;
            Assert.AreEqual(uInt8Type.Kind, fieldDict["kind"]);

            var resultInitializers = res["initializers"];
            Assert.IsInstanceOfType(resultInitializers, typeof(List<object>));
            var initializerList = resultInitializers as List<object>;

            Assert.AreEqual(0, initializerList.Count);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.StructuredTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes.TypeDefinitions
{
    [TestClass]
    public class CompositeTypeDefinitionTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsCorrectCadence()
        {
            var expectedJson = "{\"kind\":\"Resource\",\"typeID\":\"testTypeID\",\"fields\":[{\"id\":\"testField\",\"type\":{\"kind\":\"UInt8\"}}],\"initializers\":[{\"id\":\"testInit\",\"label\":\"label\",\"type\":{\"kind\":\"Int\"}}],\"type\":\"\"}";

            var typeId = "testTypeID";
            var simpleField = new SimpleTypeDefinition("UInt8");
            var simpleInitializer = new SimpleTypeDefinition("Int");

            var fields = new List<CompositeTypeDefinitionField>()
            {
                new CompositeTypeDefinitionField("testField", simpleField)
            };

            var initializers = new List<CompositeTypeDefinitionInitializer>()
            {
                new CompositeTypeDefinitionInitializer("testInit", "label", simpleInitializer)
            };

            var composite = new CompositeTypeDefinition("Resource", "", typeId, initializers, fields);

            var json = composite.AsJsonCadenceDataFormat();
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_ReturnsDictionaryWithAllProperties()
        {
            var typeId = "testTypeID";
            var simpleField = new SimpleTypeDefinition("UInt8");
            var simpleInitializer = new SimpleTypeDefinition("Int");

            var fields = new List<CompositeTypeDefinitionField>()
            {
                new CompositeTypeDefinitionField("testField", simpleField)
            };

            var initializers = new List<CompositeTypeDefinitionInitializer>()
            {
                new CompositeTypeDefinitionInitializer("testInit", "label", simpleInitializer)
            };

            var composite = new CompositeTypeDefinition("Resource", "", typeId, initializers, fields);

            var result = composite.Flatten();

            Assert.IsTrue(result.ContainsKey("kind"));
            Assert.AreEqual(result["kind"], composite.Kind);

            Assert.IsTrue(result.ContainsKey("type"));
            Assert.AreEqual(result["type"], composite.Type);

            Assert.IsTrue(result.ContainsKey("typeID"));
            Assert.AreEqual(result["typeID"], composite.TypeId);

            Assert.IsTrue(result.ContainsKey("initializers"));
            var initList = result["initializers"] as List<Dictionary<string, object>>;
            Assert.IsNotNull(initList);

            Assert.AreEqual(1, initList.Count);

            var init = initList.First();

            Assert.AreEqual(3, init.Keys.Count());
            Assert.AreEqual(init["label"], initializers.First().Label);
            Assert.AreEqual(init["id"], initializers.First().Id);

            var initType = init["type"];
            Assert.IsInstanceOfType(initType, typeof(Dictionary<string, object>));

            Assert.IsTrue(result.ContainsKey("fields"));
            var fieldList = result["fields"] as List<Dictionary<string, object>>;
            Assert.IsNotNull(fieldList);
            Assert.AreEqual(1, fieldList.Count());

            var field = fieldList.First();

            Assert.AreEqual(field["id"], fields.First().Id);
            var fieldType = field["type"];
            Assert.IsInstanceOfType(fieldType, typeof(Dictionary<string, object>));
        }
    }
}
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
            var expectedJson = "{\"kind\":\"Resource\",\"typeID\":\"compositeTypeId\",\"fields\":[{\"id\":\"fieldId\",\"type\":{\"kind\":\"Int32\"}}],\"initializers\":[{\"label\":\"initLabel\",\"id\":\"initId\",\"type\":{\"kind\":\"String\"}}],\"type\":\"\"}";

            var initType = new SimpleTypeDefinition("String");
            var initializer = new InitializerTypeDefinition("initLabel", "initId", initType);

            var fieldType = new SimpleTypeDefinition("Int32");
            var field = new FieldTypeDefinition("fieldId", fieldType);

            var composite = new CompositeTypeDefinition("Resource", "compositeTypeId", new List<InitializerTypeDefinition>() { initializer }, new List<FieldTypeDefinition>() { field });

            var json = composite.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void Flatten_HasAllProperties()
        {
            var initType = new SimpleTypeDefinition("String");
            var initializer = new InitializerTypeDefinition("initLabel", "initId", initType);

            var fieldType = new SimpleTypeDefinition("Int32");
            var field = new FieldTypeDefinition("fieldId", fieldType);

            var composite = new CompositeTypeDefinition("Resource", "compositeTypeId", new List<InitializerTypeDefinition>() { initializer }, new List<FieldTypeDefinition>() { field });

            var res = composite.Flatten();
        }
    }
}
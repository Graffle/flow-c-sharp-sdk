using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class CompositeTypeTests
    {
        [TestMethod]
        [DataRow("Struct")]
        [DataRow("Resource")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("Enum")]
        public void AsJsonCadenceDataFormat_ReturnsValidJson(string type)
        {
            var expectedJson = $"{{\"type\":\"{type}\",\"value\":{{\"fields\":[{{\"name\":\"intField\",\"value\":{{\"type\":\"Int16\",\"value\":\"123\"}}}},{{\"name\":\"stringField\",\"value\":{{\"type\":\"String\",\"value\":\"hello\"}}}}],\"id\":\"idString\"}}}}";

            var id = "idString";
            var intType = new Int16Type(123);
            var stringType = new StringType("hello");
            var fields = new List<CompositeField>()
            {
                new CompositeField("intField", intType),
                new CompositeField("stringField", stringType)
            };

            var composite = new CompositeType(type, id, fields);

            var json = composite.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
            Assert.AreEqual(id, composite.Id);
        }

        [TestMethod]
        public void DataAsJson_ReturnsValidJson()
        {
            var expectedJson = @"{""fields"":[{""name"":""intField"",""value"":{""type"":""Int16"",""value"":""123""}},{""name"":""stringField"",""value"":{""type"":""String"",""value"":""hello""}}],""id"":""idString""}";

            var id = "idString";
            var intType = new Int16Type(123);
            var stringType = new StringType("hello");
            var fields = new List<CompositeField>()
            {
                new CompositeField("intField", intType),
                new CompositeField("stringField", stringType)
            };

            var composite = new CompositeType("Struct", id, fields);

            var json = composite.DataAsJson();

            Assert.AreEqual(expectedJson, json);
            Assert.AreEqual(id, composite.Id);
        }

        [TestMethod]
        [DataRow("Struct")]
        [DataRow("Resource")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("Enum")]
        public void GivenValidJson_FromJson_ReturnsCompositeType(string type)
        {
            var json = $"{{\"type\":\"{type}\",\"value\":{{\"fields\":[{{\"name\":\"intField\",\"value\":{{\"type\":\"Int16\",\"value\":\"123\"}}}},{{\"name\":\"stringField\",\"value\":{{\"type\":\"String\",\"value\":\"hello\"}}}}],\"id\":\"idString\"}}}}";

            var composite = CompositeType.FromJson(json);
            Assert.IsNotNull(composite);
            Assert.AreEqual(type, composite.Type);

            var data = composite.Data;
            Assert.IsNotNull(data);

            var id = data.Id;
            Assert.AreEqual("idString", id);

            var fields = data.Fields;
            Assert.IsNotNull(fields);
            Assert.AreEqual(2, fields.Count);

            //verify data
            var firstName = fields[0].Name;
            Assert.AreEqual("intField", firstName);

            var firstValue = fields[0].Value;
            Assert.IsNotNull(firstValue);
            Assert.IsInstanceOfType(firstValue, typeof(Int16Type));

            var intType = firstValue as Int16Type;
            Assert.AreEqual(123, intType.Data);

            var secondName = fields[1].Name;
            Assert.AreEqual("stringField", secondName);

            var secondValue = fields[1].Value;
            Assert.IsNotNull(secondValue);
            Assert.IsInstanceOfType(secondValue, typeof(StringType));

            var stringType = secondValue as StringType;
            Assert.AreEqual("hello", stringType.Data);
        }
    }
}
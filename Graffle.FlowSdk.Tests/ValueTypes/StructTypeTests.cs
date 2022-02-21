using System;
using System.Collections.Generic;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class StructTypeTests
    {
        [TestMethod]
        public void AsJsonCadenceDataFormat_ReturnsValidJson()
        {
            var expectedJson = @"{""type"":""Struct"",""value"":{""fields"":[{""name"":""intField"",""value"":{""type"":""Int16"",""value"":""123""}},{""name"":""stringField"",""value"":{""type"":""String"",""value"":""hello""}}],""id"":""idString""}}";

            var id = "idString";
            var intType = new Int16Type(123);
            var stringType = new StringType("hello");
            var fields = new List<StructField>()
            {
                new StructField("intField", intType),
                new StructField("stringField", stringType)
            };

            var structType = new StructType(id, fields);

            var json = structType.AsJsonCadenceDataFormat();

            Assert.AreEqual(expectedJson, json);
            Assert.AreEqual(id, structType.Id);
        }

        [TestMethod]
        public void DataAsJson_ReturnsValidJson()
        {
            var expectedJson = @"{""fields"":[{""name"":""intField"",""value"":{""type"":""Int16"",""value"":""123""}},{""name"":""stringField"",""value"":{""type"":""String"",""value"":""hello""}}],""id"":""idString""}";

            var id = "idString";
            var intType = new Int16Type(123);
            var stringType = new StringType("hello");
            var fields = new List<StructField>()
            {
                new StructField("intField", intType),
                new StructField("stringField", stringType)
            };

            var structType = new StructType(id, fields);

            var json = structType.DataAsJson();

            Assert.AreEqual(expectedJson, json);
            Assert.AreEqual(id, structType.Id);
        }

        [TestMethod]
        public void GivenValidJson_FromJson_ReturnsStructType()
        {
            var json = @"{""type"":""Struct"",""value"":{""fields"":[{""name"":""intField"",""value"":{""type"":""Int16"",""value"":""123""}},{""name"":""stringField"",""value"":{""type"":""String"",""value"":""hello""}}],""id"":""idString""}}";

            var structType = StructType.FromJson(json);
            Assert.IsNotNull(structType);

            Assert.AreEqual("Struct", structType.Type);

            var data = structType.Data;
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
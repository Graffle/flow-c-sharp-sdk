using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class ArrayTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Array"",""value"": [ {""type"":""Int16"", ""value"":""123""}, {""type"":""String"",""value"":""hello world""}]}";
            var flowValueType = ArrayType.FromJson(cadenceJsonString);

            Assert.AreEqual("Array", flowValueType.Type);

            //verify array members
            var values = flowValueType.Data;
            Assert.AreEqual(2, values.Count);

            //first item
            var first = values[0];
            var intValueType = first as Int16Type;
            Assert.IsNotNull(intValueType);
            Assert.AreEqual(123, intValueType.Data);

            //second item
            var second = values[1];
            var stringValueType = second as StringType;
            Assert.IsNotNull(stringValueType);
            Assert.AreEqual("hello world", stringValueType.Data);
        }

        [TestMethod]
        public void Given_Array_Value_Create_ArrayType()
        {
            var expectedData = new List<FlowValueType>()
            {
                new Int16Type(123),
                new StringType("hello world")
            };

            var flowValueType = new ArrayType(expectedData);
            Assert.AreEqual("Array", flowValueType.Type);

            //verify array members
            var resultData = flowValueType.Data;
            Assert.AreEqual(expectedData.Count, resultData.Count);

            //first item
            Assert.AreEqual(expectedData[0].Type, resultData[0].Type);
            var resultIntType = resultData[0] as Int16Type;
            Assert.IsNotNull(resultIntType);
            Assert.AreEqual(123, resultIntType.Data);

            //second item
            Assert.AreEqual(expectedData[1].Type, resultData[1].Type);
            var resultStringType = resultData[1] as StringType;
            Assert.IsNotNull(resultStringType);
            Assert.AreEqual("hello world", resultStringType.Data);
        }

        [TestMethod]
        public void Given_ArrayType_Convert_To_Cadence_Json()
        {
            var data = new List<FlowValueType>()
            {
                new Int16Type(123),
                new StringType("hello world")
            };
            var expectedCadenceJson = @"{""type"":""Array"",""value"":[{""type"":""Int16"",""value"":""123""},{""type"":""String"",""value"":""hello world""}]}";

            var flowValueType = new ArrayType(data);

            var cadenceJson = flowValueType.AsJsonCadenceDataFormat();
            Assert.AreEqual(expectedCadenceJson, cadenceJson);
        }

        [TestMethod]
        public void Values_ReturnsCorrectValues()
        {
            var list = new List<FlowValueType>()
            {
                new Int16Type(123),
                new StringType("hello")
            };

            var arr = new ArrayType(list);

            var values = arr.Values().ToList();
            Assert.IsNotNull(values);
            Assert.AreEqual(2, values.Count);

            var first = values[0];
            Assert.IsInstanceOfType(first, typeof(Int16Type));
            var intType = first as Int16Type;
            Assert.AreEqual(123, intType.Data);

            var second = values[1];
            Assert.IsInstanceOfType(second, typeof(StringType));
            var stringType = second as StringType;
            Assert.AreEqual("hello", stringType.Data);
        }

        [TestMethod]
        [DataRow("Struct")]
        [DataRow("Resource")]
        [DataRow("Contract")]
        [DataRow("Event")]
        [DataRow("Enum")]
        public void FromJson_NestedCompositeType_ReturnsArrayType(string nestedCompositeType)
        {
            /*
            {
                "type":"Array",
                "value":[
                    {
                        "type":"Struct",
                        "value": {
                            "id":"structId",
                            "fields": [
                                {
                                    "name":"structField1",
                                    "value": {
                                        "type":"Int",
                                        "value":2
                                    }
                                }
                            ]
                        }
                    }
                ]
            }
            */

            var json = $"{{\"type\":\"Array\",\"value\":[{{\"type\":\"{nestedCompositeType}\",\"value\":{{\"id\":\"structId\",\"fields\":[{{\"name\":\"structField1\",\"value\":{{\"type\":\"Int\",\"value\":\"2\"}}}}]}}}}]}}";

            var arr = ArrayType.FromJson(json);

            var arrItem = arr.Data[0];
            Assert.IsInstanceOfType(arrItem, typeof(CompositeType));

            var composite = arrItem as CompositeType;

            Assert.AreEqual(nestedCompositeType, composite.Type);
            Assert.AreEqual("structId", composite.Id);

            var compositeFields = composite.Fields;
            Assert.AreEqual(1, compositeFields.Count());

            var first = compositeFields.First();
            Assert.AreEqual("structField1", first.Name);

            var structFieldValue = first.Value;
            Assert.IsInstanceOfType(structFieldValue, typeof(IntType));

            var intType = structFieldValue as IntType;
            Assert.AreEqual(2, intType.Data);
        }
    }
}
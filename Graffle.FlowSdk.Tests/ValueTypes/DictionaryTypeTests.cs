using System;
using System.Collections.Generic;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class DictionaryTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var key = new UInt8Type(123);
            var cadenceJsonString = @"{""type"":""Dictionary"",""value"":[{""key"":{""type"":""UInt8"",""value"":""123""},""value"":{""type"":""String"",""value"":""test""}}]}";
            var flowValueType = DictionaryType.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Dictionary");
            var found = flowValueType.TryGetValueType(key, out var item);
            var expected = new StringType("test");
            Assert.AreEqual(expected.Data, ((StringType)item).Data);
            Assert.AreEqual(expected.Type, ((StringType)item).Type);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Create_Dictionary_With_Flow_Value_Types()
        {
            var test = new Dictionary<FlowValueType, FlowValueType>();
            test.Add(new UInt8Type(123), new StringType("test"));
            test.Add(new StringType("test"), new UInt8Type(123));
            test.Add(new UInt16Type(45345), new StringType("TESDTDSAF DSDFDAF FASF"));
            var dict = new DictionaryType(test);
            var found = dict.TryGetValueType(new UInt16Type(45345), out var item);
            Assert.AreEqual(3, dict.Data.Count);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Given_DictionaryType_Convert_To_Cadence_Json()
        {
            var key = new UInt8Type(123);
            var cadenceJsonString = @"{""type"":""Dictionary"",""value"":[{""key"":{""type"":""UInt8"",""value"":""123""},""value"":{""type"":""String"",""value"":""test""}},{""key"":{""type"":""UInt8"",""value"":""124""},""value"":{""type"":""String"",""value"":""test2""}}]}";
            var flowValueType = DictionaryType.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Dictionary");
            var found = flowValueType.TryGetValueType(key, out var item);
            var expected = new StringType("test");
            Assert.AreEqual(((StringType)item).Data, expected.Data);
            Assert.AreEqual(((StringType)item).Type, expected.Type);
            Assert.IsTrue(found);

            var convertedBack = flowValueType.AsJsonCadenceDataFormat();
            Assert.AreEqual(cadenceJsonString, convertedBack);
        }

        [TestMethod]
        public void TryGetValueType_KeyNotFound_ReturnsFalse()
        {
            var intType = new Int16Type(123);
            var stringType = new StringType("hello");

            var dict = new Dictionary<FlowValueType, FlowValueType>()
            {
                { intType, stringType }
            };

            var dictType = new DictionaryType(dict);

            //test method
            var key = new Int16Type(1234);
            var result = dictType.TryGetValueType(key, out var value);

            Assert.IsFalse(result);
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TryGetValueType_EmptyDictionary_ReturnsFalse()
        {
            var dict = new Dictionary<FlowValueType, FlowValueType>();

            var dictType = new DictionaryType(dict);
            var key = new Int16Type(123);

            var result = dictType.TryGetValueType(key, out var value);
            Assert.IsFalse(result);
            Assert.IsNull(value);
        }
    }
}

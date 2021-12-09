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
            Assert.AreEqual(((StringType)item).Data, expected.Data);
            Assert.AreEqual(((StringType)item).Type, expected.Type);
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
            Assert.AreEqual(dict.Data.Count, 3);
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
            Assert.AreEqual(cadenceJsonString,convertedBack);
        }
    }
}

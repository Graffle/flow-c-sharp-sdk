using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Word32TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word32()
        {
            var cadenceJsonString = @"{""type"":""Word32"",""value"":""100""}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word32", flowValueType.Type);
            Assert.AreEqual((UInt64)100, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word32()
        {
            UInt32 value = 4294967295;
            var cadenceJsonString = $"{{\"type\":\"Word32\",\"value\":\"{value}\"}}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word32", flowValueType.Type);
            Assert.AreEqual(value, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word32()
        {
            UInt32 value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word32\",\"value\":\"{value}\"}}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word32", flowValueType.Type);
            Assert.AreEqual(value, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Word32_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word32Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word32"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}
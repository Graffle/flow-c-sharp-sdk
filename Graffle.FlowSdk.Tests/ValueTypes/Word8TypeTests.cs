using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Word8TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word8()
        {
            var cadenceJsonString = @"{""type"":""Word8"",""value"":""100""}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word8", flowValueType.Type);
            Assert.AreEqual((UInt64)100, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word8()
        {
            uint value = 255;
            var cadenceJsonString = $"{{\"type\":\"Word8\",\"value\":\"{value}\"}}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word8", flowValueType.Type);
            Assert.AreEqual(value, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word8()
        {
            uint value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word8\",\"value\":\"{value}\"}}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Word8", flowValueType.Type);
            Assert.AreEqual(value, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Word8_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word8Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word8"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}
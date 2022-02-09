using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Word16TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word16()
        {
            var cadenceJsonString = @"{""type"":""Word16"",""value"":""100""}";
            var flowValueType = Word16Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word16");
            Assert.AreEqual(flowValueType.Data, (UInt64)100);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word16()
        {
            uint value = 65535;
            var cadenceJsonString = $"{{\"type\":\"Word16\",\"value\":\"{value}\"}}";
            var flowValueType = Word16Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word16");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word16()
        {
            uint value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word16\",\"value\":\"{value}\"}}";
            var flowValueType = Word16Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word16");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Word16_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word16Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word16"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}
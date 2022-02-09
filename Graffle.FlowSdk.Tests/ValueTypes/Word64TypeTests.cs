using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class WordTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word64()
        {
            var cadenceJsonString = @"{""type"":""Word64"",""value"":""100""}";
            var flowValueType = Word64Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word64");
            Assert.AreEqual(flowValueType.Data, (UInt64)100);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word64()
        {
            UInt64 value = 18446744073709551615;
            var cadenceJsonString = $"{{\"type\":\"Word64\",\"value\":\"{value}\"}}";
            var flowValueType = Word64Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word64");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word64()
        {
            UInt64 value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word64\",\"value\":\"{value}\"}}";
            var flowValueType = Word64Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word64");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Word64_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word64Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word64"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

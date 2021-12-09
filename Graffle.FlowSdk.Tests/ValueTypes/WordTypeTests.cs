using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class WordTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word8()
        {
            var cadenceJsonString = @"{""type"":""Word8"",""value"":""100""}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word8");
            Assert.AreEqual(flowValueType.Data, (UInt64)100);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word8()
        {
            uint value = 255;
            var cadenceJsonString = $"{{\"type\":\"Word8\",\"value\":\"{value}\"}}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word8");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word8()
        {
            uint value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word8\",\"value\":\"{value}\"}}";
            var flowValueType = Word8Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word8");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Word8_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word8Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word8"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }

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



        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_Word32()
        {
            var cadenceJsonString = @"{""type"":""Word32"",""value"":""100""}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word32");
            Assert.AreEqual(flowValueType.Data, (UInt64)100);
        }

        [TestMethod]
        public void Given_Max_Value_Cadence_Json_Create_Word32()
        {
            UInt32 value = 4294967295;
            var cadenceJsonString = $"{{\"type\":\"Word32\",\"value\":\"{value}\"}}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word32");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Min_Value_Cadence_Json_Create_Word32()
        {
            UInt32 value = 0;
            var cadenceJsonString = $"{{\"type\":\"Word32\",\"value\":\"{value}\"}}";
            var flowValueType = Word32Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Word32");
            Assert.AreEqual(flowValueType.Data, value);
        }

        [TestMethod]
        public void Given_Word32_Convert_To_Cadence_Json()
        {
            var flowValueType = new Word32Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Word32"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }

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

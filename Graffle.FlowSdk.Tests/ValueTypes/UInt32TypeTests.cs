using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt32TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""UInt32"",""value"":""100""}";
            var flowValueType = UInt32Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "UInt32");
            Assert.AreEqual(flowValueType.Data, (UInt32)100);
        }

        [TestMethod]
        public void Given_Value_Create_UInt32Type()
        {
            var flowValueType = new UInt32Type(100);
            Assert.AreEqual(flowValueType.Type, "UInt32");
            Assert.AreEqual(flowValueType.Data, (UInt32)100);
        }

        [TestMethod]
        public void Given_UInt32_Convert_To_Cadence_Json()
        {
            var flowValueType = new UInt32Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""UInt32"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected,cadenceValue);
        }
    }
}

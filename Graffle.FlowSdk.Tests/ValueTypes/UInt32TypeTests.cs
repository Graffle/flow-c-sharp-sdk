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
            Assert.AreEqual("UInt32", flowValueType.Type);
            Assert.AreEqual((UInt32)100, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt32_Value_Create_UInt32Type()
        {
            uint expected = 100;

            var flowValueType = new UInt32Type(expected);
            Assert.AreEqual("UInt32", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UInt32Type()
        {
            uint expected = 100;

            var flowValueType = new UInt32Type(expected.ToString());
            Assert.AreEqual("UInt32", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt32_Convert_To_Cadence_Json()
        {
            var flowValueType = new UInt32Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""UInt32"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

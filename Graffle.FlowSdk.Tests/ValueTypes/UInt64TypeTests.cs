using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt64TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""UInt64"",""value"":""100""}";
            var flowValueType = UInt64Type.FromJson(cadenceJsonString);
            Assert.AreEqual("UInt64", flowValueType.Type);
            Assert.AreEqual((UInt64)100, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt64_Value_Create_UInt64Type()
        {
            ulong expected = 100;

            var flowValueType = new UInt64Type(expected);
            Assert.AreEqual("UInt64", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UInt64Type()
        {
            ulong expected = 100;

            var flowValueType = new UInt64Type(expected.ToString());
            Assert.AreEqual("UInt64", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt32_Convert_To_Cadence_Json()
        {
            var flowValueType = new UInt64Type(100);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""UInt64"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

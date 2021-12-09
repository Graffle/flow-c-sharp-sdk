using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UFix64TypeTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""UFix64"",""value"":""100""}";
            var flowValueType = UInt32Type.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "UInt32");
            Assert.AreEqual(flowValueType.Data, 100m);
        }

        [TestMethod]
        public void Given_Value_Create_UFix64Type()
        {
            var flowValueType = new UFix64Type(100m);
            Assert.AreEqual(flowValueType.Type, "UFix64");
            Assert.AreEqual(flowValueType.Data, 100m);
        }

        [TestMethod]
        public void Given_UFix64_Convert_To_Cadence_Json()
        {
            var flowValueType = new UFix64Type(100m);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""UFix64"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected,cadenceValue);
        }
    }
}

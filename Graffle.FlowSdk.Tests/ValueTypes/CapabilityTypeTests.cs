using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class CapabilityTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Capability"",""value"":{""path"":""/public/someInteger"",""address"":""0x1"",""borrowType"":""Int""}}";
            var flowValueType = CapabilityType.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Capability");
            Assert.AreEqual(flowValueType.Data.Count, 3);
            Assert.AreEqual(flowValueType.Path, "/public/someInteger");
            Assert.AreEqual(flowValueType.Address, "0x1");
            Assert.AreEqual(flowValueType.BorrowType, "Int");
        }

        [TestMethod]
        public void Given_PathType_Value_Create_PathType()
        {
            var flowValueType = new CapabilityType("/public/someInteger", "0x1", "Int");
            Assert.AreEqual(flowValueType.Type, "Capability");
            Assert.AreEqual(flowValueType.Data.Count, 3);
            Assert.AreEqual(flowValueType.Path, "/public/someInteger");
            Assert.AreEqual(flowValueType.Address, "0x1");
            Assert.AreEqual(flowValueType.BorrowType, "Int");
        }

        [TestMethod]
        public void Given_BoolType_Convert_To_Cadence_Json()
        {
             var flowValueType = new CapabilityType("/public/someInteger", "0x1", "Int");
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Capability"",""value"":{""path"":""/public/someInteger"",""address"":""0x1"",""borrowType"":""Int""}}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

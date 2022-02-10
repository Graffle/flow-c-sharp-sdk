using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class CapabilityTypeTests
    {
        [TestMethod]
        [DataRow(@"{""type"":""Capability"",""value"":{""path"":""/public/someInteger"",""address"":""0x1"",""borrowType"":""Int""}}")]
        [DataRow(@"{""path"": ""/public/someInteger"",""address"": ""0x1"",""borrowType"": ""Int""}")]
        public void Given_Valid_Cadence_Json_Create_ValueType(string cadenceJsonString)
        {
            var flowValueType = CapabilityType.FromJson(cadenceJsonString);
            Assert.AreEqual("Capability", flowValueType.Type);
            Assert.AreEqual(3, flowValueType.Data.Count);
            Assert.AreEqual("/public/someInteger", flowValueType.Path);
            Assert.AreEqual("0x1", flowValueType.Address);
            Assert.AreEqual("Int", flowValueType.BorrowType);
        }

        [TestMethod]
        public void Given_PathType_Value_Create_PathType()
        {
            var flowValueType = new CapabilityType("/public/someInteger", "0x1", "Int");
            Assert.AreEqual("Capability", flowValueType.Type);
            Assert.AreEqual(3, flowValueType.Data.Count);
            Assert.AreEqual("/public/someInteger", flowValueType.Path);
            Assert.AreEqual("0x1", flowValueType.Address);
            Assert.AreEqual("Int", flowValueType.BorrowType);
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

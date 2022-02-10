using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Fix64TypeTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Fix64"",""value"":""100""}";
            var flowValueType = Fix64Type.FromJson(cadenceJsonString);
            Assert.AreEqual("Fix64", flowValueType.Type);
            Assert.AreEqual(100m, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Value_Create_Fix64Type()
        {
            var flowValueType = new Fix64Type(100m);
            Assert.AreEqual("Fix64", flowValueType.Type);
            Assert.AreEqual(100m, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Fix64_Convert_To_Cadence_Json()
        {
            var flowValueType = new Fix64Type(100m);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Fix64"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

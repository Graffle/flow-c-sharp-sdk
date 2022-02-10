using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class BoolTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Bool"",""value"":true}";
            var flowValueType = BoolType.FromJson(cadenceJsonString);
            Assert.AreEqual("Bool", flowValueType.Type);
            Assert.AreEqual(true, flowValueType.Data);
        }

        [TestMethod]
        public void Given_BoolType_Value_Create_StringType()
        {
            var flowValueType = new BoolType(true);
            Assert.AreEqual("Bool", flowValueType.Type);
            Assert.AreEqual(true, flowValueType.Data);
        }

        [TestMethod]
        public void Given_BoolType_Convert_To_Cadence_Json()
        {
            var flowValueType = new BoolType(true);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Bool"",""value"":true}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

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
            var flowValueType = UFix64Type.FromJson(cadenceJsonString);
            Assert.AreEqual("UFix64", flowValueType.Type);
            Assert.AreEqual(100m, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Decimal_Value_Create_UFix64Type()
        {
            var expected = 100m;
            var flowValueType = new UFix64Type(expected);
            Assert.AreEqual("UFix64", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UFix64Type()
        {
            var expected = 100m;
            var flowValueType = new UFix64Type(expected.ToString());

            Assert.AreEqual("UFix64", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UFix64_Convert_To_Cadence_Json()
        {
            var flowValueType = new UFix64Type(100m);
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""UFix64"",""value"":""100""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

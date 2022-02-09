using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt128TypeTests
    {
        private const string VALUE = "91389681247993671255432112000000000";
        private static readonly BigInteger BIGINT = BigInteger.Parse(VALUE);

        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""UInt128"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = UInt128Type.FromJson(cadenceJson);

            Assert.AreEqual("UInt128", flowValueType.Type);
            Assert.AreEqual(BIGINT, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt128_Value_Create_UInt128Type()
        {
            var expected = BIGINT;

            var flowValueType = new UInt128Type(expected);
            Assert.AreEqual("UInt128", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt128Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""UInt128"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = new UInt128Type(BIGINT);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt256TypeTests
    {
        private const string VALUE = "91389681247993671255432112000000000";
        private static readonly BigInteger BIGINT = BigInteger.Parse(VALUE);

        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""UInt256"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = UInt256Type.FromJson(cadenceJson);

            Assert.AreEqual("UInt256", flowValueType.Type);
            Assert.AreEqual(BIGINT, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt256_Value_Create_UInt256Type()
        {
            var expected = BIGINT;

            var flowValueType = new UInt256Type(expected);
            Assert.AreEqual("UInt256", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UInt256Type()
        {
            var expected = BIGINT;

            var flowValueType = new UInt256Type(expected.ToString());
            Assert.AreEqual("UInt256", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt256Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""UInt256"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = new UInt256Type(BIGINT);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
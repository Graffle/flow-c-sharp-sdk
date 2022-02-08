using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int256TypeTests
    {
        private const string VALUE = "91389681247993671255432112000000000";
        private static readonly BigInteger BIGINT = BigInteger.Parse(VALUE);

        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int256"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = Int256Type.FromJson(cadenceJson);

            Assert.AreEqual("Int256", flowValueType.Type);
            Assert.AreEqual(BIGINT, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int256_Value_Create_Int256Type()
        {
            var expected = BIGINT;

            var flowValueType = new Int256Type(expected);
            Assert.AreEqual("Int256", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int256Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int256"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = new Int256Type(BIGINT);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
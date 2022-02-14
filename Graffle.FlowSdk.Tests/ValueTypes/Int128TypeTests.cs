using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int128TypeTests
    {
        private const string VALUE = "91389681247993671255432112000000000";
        private static readonly BigInteger BIGINT = BigInteger.Parse(VALUE);

        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int128"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = Int128Type.FromJson(cadenceJson);

            Assert.AreEqual("Int128", flowValueType.Type);
            Assert.AreEqual(BIGINT, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int128_Value_Create_Int128Type()
        {
            var expected = BIGINT;

            var flowValueType = new Int128Type(expected);
            Assert.AreEqual("Int128", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_Int128Type()
        {
            var expected = BIGINT;

            var flowValueType = new Int128Type(expected.ToString());
            Assert.AreEqual("Int128", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int128Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int128"",""value"":""91389681247993671255432112000000000""}";
            var flowValueType = new Int128Type(BIGINT);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
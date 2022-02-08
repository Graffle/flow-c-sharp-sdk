using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int64TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int64"",""value"":""0""}";
            var flowValueType = Int64Type.FromJson(cadenceJson);

            Assert.AreEqual("Int64", flowValueType.Type);
            Assert.AreEqual(0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int64_Value_Create_Int64Type()
        {
            long expected = long.MaxValue;

            var flowValueType = new Int64Type(expected);
            Assert.AreEqual("Int64", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int64Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int64"",""value"":""0""}";
            var flowValueType = new Int64Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
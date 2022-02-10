using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int8TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int8"",""value"":""0""}";
            var flowValueType = Int8Type.FromJson(cadenceJson);

            Assert.AreEqual("Int8", flowValueType.Type);
            Assert.AreEqual(0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int8_Value_Create_Int8Type()
        {
            int expected = 0;

            var flowValueType = new Int8Type(expected);
            Assert.AreEqual("Int8", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_Int8Type()
        {
            int expected = 0;

            var flowValueType = new Int8Type(expected.ToString());
            Assert.AreEqual("Int8", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int8Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int8"",""value"":""0""}";
            var flowValueType = new Int8Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int32TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int32"",""value"":""0""}";
            var flowValueType = Int32Type.FromJson(cadenceJson);

            Assert.AreEqual("Int32", flowValueType.Type);
            Assert.AreEqual(0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int32_Value_Create_Int32Type()
        {
            int expected = 12345;

            var flowValueType = new Int32Type(expected);
            Assert.AreEqual("Int32", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_Int32Type()
        {
            int expected = 12345;

            var flowValueType = new Int32Type(expected.ToString());
            Assert.AreEqual("Int32", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int32Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int32"",""value"":""0""}";
            var flowValueType = new Int32Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
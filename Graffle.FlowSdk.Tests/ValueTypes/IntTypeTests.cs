using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class IntTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int"",""value"":""0""}";
            var flowValueType = IntType.FromJson(cadenceJson);

            Assert.AreEqual("Int", flowValueType.Type);
            Assert.AreEqual(0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int_Value_Create_IntType()
        {
            int expected = 12345;

            var flowValueType = new IntType(expected);
            Assert.AreEqual("Int", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_IntType()
        {
            int expected = 12345;

            var flowValueType = new IntType(expected.ToString());
            Assert.AreEqual("Int", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_IntType_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int"",""value"":""0""}";
            var flowValueType = new IntType(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
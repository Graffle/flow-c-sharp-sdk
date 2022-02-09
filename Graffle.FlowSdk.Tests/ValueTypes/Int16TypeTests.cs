using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class Int16TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""Int16"",""value"":""0""}";
            var flowValueType = Int16Type.FromJson(cadenceJson);

            Assert.AreEqual("Int16", flowValueType.Type);
            Assert.AreEqual(0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int16_Value_Create_Int16Type()
        {
            short expected = short.MaxValue;

            var flowValueType = new Int16Type(expected);
            Assert.AreEqual("Int16", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_Int16Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""Int16"",""value"":""0""}";
            var flowValueType = new Int16Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
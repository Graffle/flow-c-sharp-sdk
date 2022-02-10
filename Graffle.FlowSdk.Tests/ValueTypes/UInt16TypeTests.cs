using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt16TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""UInt16"",""value"":""0""}";
            var flowValueType = UInt16Type.FromJson(cadenceJson);

            Assert.AreEqual("UInt16", flowValueType.Type);
            Assert.AreEqual((uint)0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt16_Value_Create_UInt16Type()
        {
            uint expected = 0;

            var flowValueType = new UInt16Type(expected);
            Assert.AreEqual("UInt16", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UInt16Type()
        {
            uint expected = 0;

            var flowValueType = new UInt16Type(expected.ToString());
            Assert.AreEqual("UInt16", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt16Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""UInt16"",""value"":""0""}";
            var flowValueType = new UInt16Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
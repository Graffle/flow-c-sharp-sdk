using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UInt8TypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""UInt8"",""value"":""0""}";
            var flowValueType = UInt8Type.FromJson(cadenceJson);

            Assert.AreEqual("UInt8", flowValueType.Type);
            Assert.AreEqual((uint)0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt8_Value_Create_UInt8Type()
        {
            uint expected = 12345;

            var flowValueType = new UInt8Type(expected);
            Assert.AreEqual("UInt8", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_UInt8Type()
        {
            uint expected = 12345;

            var flowValueType = new UInt8Type(expected.ToString());
            Assert.AreEqual("UInt8", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt8Type_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""UInt8"",""value"":""0""}";
            var flowValueType = new UInt8Type(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class UIntTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJson = @"{""type"":""UInt"",""value"":""0""}";
            var flowValueType = UIntType.FromJson(cadenceJson);

            Assert.AreEqual("UInt", flowValueType.Type);
            Assert.AreEqual((uint)0, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UInt_Value_Create_UIntType()
        {
            uint expected = 0;

            var flowValueType = new UIntType(expected);
            Assert.AreEqual("UInt", flowValueType.Type);
            Assert.AreEqual(expected, flowValueType.Data);
        }

        [TestMethod]
        public void Given_UIntType_Convert_To_Cadence_Json()
        {
            var expectedCadenceJson = @"{""type"":""UInt"",""value"":""0""}";
            var flowValueType = new UIntType(0);

            Assert.AreEqual(expectedCadenceJson, flowValueType.AsJsonCadenceDataFormat());
        }
    }
}
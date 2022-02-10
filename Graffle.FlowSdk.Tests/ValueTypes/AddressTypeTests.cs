using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class AddressTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Address"",""value"":""0x66d6f450e25a4e22""}";
            var flowValueType = AddressType.FromJson(cadenceJsonString);
            Assert.AreEqual("Address", flowValueType.Type);
            Assert.AreEqual("0x66d6f450e25a4e22", flowValueType.Data);
        }

        [TestMethod]
        public void Given_Address_Value_Create_StringType()
        {
            var flowValueType = new AddressType("0x66d6f450e25a4e22");
            Assert.AreEqual("Address", flowValueType.Type);
            Assert.AreEqual("0x66d6f450e25a4e22", flowValueType.Data);
        }

        [TestMethod]
        public void Given_AddressType_Convert_To_Cadence_Json()
        {
            var flowValueType = new AddressType("0x66d6f450e25a4e22");
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Address"",""value"":""0x66d6f450e25a4e22""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class StringTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""String"",""value"":""Test""}";
            var flowValueType = StringType.FromJson(cadenceJsonString);
            Assert.AreEqual("String", flowValueType.Type);
            Assert.AreEqual("Test", flowValueType.Data);
        }

        [TestMethod]
        public void Given_String_Value_Create_StringType()
        {
            var flowValueType = new StringType("Test");
            Assert.AreEqual("String", flowValueType.Type);
            Assert.AreEqual("Test", flowValueType.Data);
        }

        [TestMethod]
        public void Given_StringType_Convert_To_Cadence_Json()
        {
            var flowValueType = new StringType("Test");
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""String"",""value"":""Test""}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

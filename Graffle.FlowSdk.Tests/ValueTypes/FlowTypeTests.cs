using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FlowTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Type"",""value"":{""staticType"": ""A.3c64bbf9200963d9.FazeUtilityCoin.Vault""}}";
            var flowValueType = FlowType.FromJson(cadenceJsonString);
            Assert.AreEqual("Type", flowValueType.Type);
            Assert.AreEqual("A.3c64bbf9200963d9.FazeUtilityCoin.Vault", flowValueType.Data);
        }

        [TestMethod]
        public void Given_Type_Value_Create_FlowType()
        {
            var flowValueType = new FlowType("A.3c64bbf9200963d9.FazeUtilityCoin.Vault");
            Assert.AreEqual("Type", flowValueType.Type);
            Assert.AreEqual("A.3c64bbf9200963d9.FazeUtilityCoin.Vault", flowValueType.Data);
        }

        [TestMethod]
        public void Given_FlowType_Convert_To_Cadence_Json()
        {
            var flowValueType = new FlowType("A.3c64bbf9200963d9.FazeUtilityCoin.Vault");
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Type"",""value"":{""staticType"":""A.3c64bbf9200963d9.FazeUtilityCoin.Vault""}}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

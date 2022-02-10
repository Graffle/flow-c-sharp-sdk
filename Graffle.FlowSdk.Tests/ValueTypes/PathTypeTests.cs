using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class PathTypeTests
    {
        [TestMethod]
        [DataRow(@"{""type"":""Path"",""value"":{""domain"":""testDomain"",""identifier"":""testIdentifier""}}")]
        [DataRow(@"{""domain"": ""testDomain"",""identifier"": ""testIdentifier""}")]
        public void Given_Valid_Cadence_Json_Create_ValueType(string cadenceJsonString)
        {
            var flowValueType = PathType.FromJson(cadenceJsonString);
            Assert.AreEqual("Path", flowValueType.Type);
            Assert.AreEqual(2, flowValueType.Data.Count);
            Assert.AreEqual("testDomain", flowValueType.Domain);
            Assert.AreEqual("testIdentifier", flowValueType.Identifier);
        }

        [TestMethod]
        public void Given_PathType_Value_Create_PathType()
        {
            var flowValueType = new PathType("testDomain", "testIdentifier");
            Assert.AreEqual("Path", flowValueType.Type);
            Assert.AreEqual(2, flowValueType.Data.Count);
            Assert.AreEqual("testDomain", flowValueType.Domain);
            Assert.AreEqual("testIdentifier", flowValueType.Identifier);
        }

        [TestMethod]
        public void Given_BoolType_Convert_To_Cadence_Json()
        {
            var flowValueType = new PathType("testDomain", "testIdentifier");
            var cadenceValue = flowValueType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Path"",""value"":{""domain"":""testDomain"",""identifier"":""testIdentifier""}}";
            Assert.AreEqual(cadenceExpected, cadenceValue);
        }
    }
}

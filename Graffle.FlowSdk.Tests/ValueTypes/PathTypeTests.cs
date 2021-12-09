using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class PathTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Path"",""value"":{""domain"":""testDomain"",""identifier"":""testIdentifier""}}";
            var flowValueType = PathType.FromJson(cadenceJsonString);
            Assert.AreEqual(flowValueType.Type, "Path");
            Assert.AreEqual(flowValueType.Data.Count, 2);
            Assert.AreEqual(flowValueType.Domain, "testDomain");
            Assert.AreEqual(flowValueType.Identifier, "testIdentifier");
        }

        [TestMethod]
        public void Given_PathType_Value_Create_PathType()
        {
            var flowValueType = new PathType("testDomain", "testIdentifier");
            Assert.AreEqual(flowValueType.Type, "Path");
            Assert.AreEqual(flowValueType.Data.Count, 2);
            Assert.AreEqual(flowValueType.Domain, "testDomain");
            Assert.AreEqual(flowValueType.Identifier, "testIdentifier");
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

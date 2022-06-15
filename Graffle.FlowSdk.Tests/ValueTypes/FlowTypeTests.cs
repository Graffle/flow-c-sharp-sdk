using System;
using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.StructuredTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FlowTypeTests
    {
        [TestMethod]
        [DataRow("Resource")]
        [DataRow("Struct")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("StructInterface")]
        [DataRow("ResourceInterface")]
        [DataRow("ContractInterface")]
        public void CompositeTypeDefinition_ReturnsCompositeTypeDefinition(string kind)
        {
            var json = $"{{\"type\":\"Type\",\"value\":{{\"staticType\":{{\"kind\":\"{kind}\",\"typeID\":\"A.ff68241f0f4fd521.DrSeuss.NFT\",\"fields\":[{{\"id\":\"uuid\",\"type\":{{\"kind\":\"UInt64\"}}}},{{\"id\":\"id\",\"type\":{{\"kind\":\"UInt64\"}}}},{{\"id\":\"mintNumber\",\"type\":{{\"kind\":\"UInt32\"}}}},{{\"id\":\"contentCapability\",\"type\":{{\"kind\":\"Capability\",\"type\":\"\"}}}},{{\"id\":\"contentId\",\"type\":{{\"kind\":\"String\"}}}}],\"initializers\":[],\"type\":\"\"}}}}}}";

            var result = FlowType.FromJson(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            //break down the inner type definition and make sure it's all good
            var data = result.Data;
            Assert.IsInstanceOfType(data, typeof(CompositeTypeDefinition));

            Assert.AreEqual(kind, data.Kind);

            var composite = data as CompositeTypeDefinition;
            Assert.AreEqual("A.ff68241f0f4fd521.DrSeuss.NFT", composite.TypeId);

            var fields = composite.Fields;
            Assert.IsNotNull(fields);
            Assert.AreEqual(5, fields.Count);

            Assert.AreEqual(string.Empty, composite.Type); //composite definitions do not have a type

            var initializers = composite.Initializers;
            Assert.IsNotNull(initializers);
            Assert.AreEqual(0, initializers.Count);
        }

        [TestMethod]
        [DataRow("UInt8")]
        [DataRow("Address")]
        [DataRow("String")]
        public void SimpleType_ReturnsSimpleType(string kind)
        {
            var json = $"{{\"type\":\"Type\",\"staticType\":{{\"kind\":\"{kind}\"}}}}";

            var result = FlowType.FromJson(json);
            Assert.IsNotNull(result);
            Assert.AreEqual("Type", result.Type);

            var data = result.Data;

            Assert.IsNotNull(data);
            Assert.IsInstanceOfType(data, typeof(SimpleTypeDefinition));

            var simple = data as SimpleTypeDefinition;

            Assert.AreEqual(kind, simple.Kind);
        }

        [TestMethod]
        public void CapabilityType_ReturnsCapabilityType()
        {

        }

    }
}

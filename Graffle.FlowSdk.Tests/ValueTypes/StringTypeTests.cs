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

        [TestMethod]
        public void FromJson_NewLine_ReturnsStringType()
        {
            var json = "{\"type\":\"String\",\"value\":\"Appearance: The xG Reward for players with game time in a fixture.\n\nGet xG Rewards for your football achievements.\nBuild your collection - your story.\nUnlock xG experiences.\n\nhttps://linktr.ee/xgstudios\"}";

            var res = StringType.FromJson(json);

            Assert.IsNotNull(res);

            var data = res.Data;
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Length > 0);

            Assert.IsFalse(data.Contains("\\n"));
            Assert.IsTrue(data.Contains("\n"));
        }
    }
}

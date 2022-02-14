using System;
using Graffle.FlowSdk.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class OptionalTypeTests
    {
        [TestMethod]
        public void Given_Null_Value_Create_Optional_Type_With_Null()
        {
            var optionalType = new OptionalType(null);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.IsNull(optionalType.Data);
        }

        [TestMethod]
        public void Given_Null_Value_Create_Optional_Type_Cadence_Json()
        {
            var optionalType = new OptionalType(null);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Optional"",""value"":null}";
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.IsNull(optionalType.Data);
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_Address_Type_With_Value_Create_Optional_Type_With_Value()
        {
            var valueType = new AddressType("0x66d6f450e25a4e22");
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_Address_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            var valueType = new AddressType("0x66d6f450e25a4e22");
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Optional"",""value"":{""type"":""Address"",""value"":""0x66d6f450e25a4e22""}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_String_Type_With_Value_Create_Optional_Type_With_Value()
        {
            var valueType = new StringType("Hello World");
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_String_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            var valueType = new StringType("Hello World");
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = @"{""type"":""Optional"",""value"":{""type"":""String"",""value"":""Hello World""}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_UFIX64_Type_With_Value_Create_Optional_Type_With_Value()
        {
            var value = 10000;
            var valueType = new UFix64Type(value);
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_UFIX64_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            var value = 10000;
            var valueType = new UFix64Type(value);
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = $"{{\"type\":\"Optional\",\"value\":{{\"type\":\"UFix64\",\"value\":\"{value}\"}}}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_UInt32_Type_With_Value_Create_Optional_Type_With_Value()
        {
            uint value = 10000;
            var valueType = new UInt32Type(value);
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_UInt32_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            uint value = 10000;
            var valueType = new UInt32Type(value);
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = $"{{\"type\":\"Optional\",\"value\":{{\"type\":\"UInt32\",\"value\":\"{value}\"}}}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_UInt64_Type_With_Value_Create_Optional_Type_With_Value()
        {
            UInt64 value = 10000;
            var valueType = new UInt64Type(value);
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_UInt64_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            UInt64 value = 10000;
            var valueType = new UInt64Type(value);
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = $"{{\"type\":\"Optional\",\"value\":{{\"type\":\"UInt64\",\"value\":\"{value}\"}}}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_Bool_Type_With_Value_Create_Optional_Type_With_Value()
        {
            var value = true;
            var valueType = new BoolType(value);
            var optionalType = new OptionalType(valueType);
            Assert.AreEqual("Optional", optionalType.Type);
            Assert.AreEqual(valueType, optionalType.Data);
        }

        [TestMethod]
        public void Given_Bool_Type_With_Value_Create_Optional_Type_Cadence_Json()
        {
            var value = false;
            var valueType = new BoolType(value);
            var optionalType = new OptionalType(valueType);
            var cadence = optionalType.AsJsonCadenceDataFormat();
            var cadenceExpected = $"{{\"type\":\"Optional\",\"value\":{{\"type\":\"Bool\",\"value\":false}}}}";
            Assert.AreEqual(cadenceExpected, cadence);
        }

        [TestMethod]
        public void Given_ValidCadenceJson_CreatesOptionalType()
        {
            var cadenceJson = $"{{\"type\":\"Optional\",\"value\":{{\"type\":\"Int16\",\"value\":123}}}}";

            var result = OptionalType.FromJson(cadenceJson);

            Assert.IsInstanceOfType(result.Data, typeof(Int16Type));

            var data = result.Data as Int16Type;

            Assert.AreEqual(123, data.Data);
        }

        [TestMethod]
        public void Given_NilJson_CreatesOptionalType()
        {
            var cadenceJson = "{\"type\":\"Optional\",\"value\":null}";

            var result = OptionalType.FromJson(cadenceJson);

            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void FromJson_WithNestedType_CreatesOptionalType()
        {
            var json = @"{""type"":""Optional"",""value"":{""type"":""Type"",""value"":{""staticType"":""A.ca4ee530dafff8ad.Evolution.NFT""}}}";

            var result = OptionalType.FromJson(json);

            Assert.IsInstanceOfType(result, typeof(OptionalType));

            var opt = result as OptionalType;

            Assert.IsInstanceOfType(opt.Data, typeof(FlowType));

            var type = opt.Data as FlowType;

            Assert.AreEqual("A.ca4ee530dafff8ad.Evolution.NFT", type.Data);
        }
    }
}

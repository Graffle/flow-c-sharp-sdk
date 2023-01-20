using Graffle.FlowSdk.Types;
using Graffle.FlowSdk.Types.TypeDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Graffle.FlowSdk.Tests.ValueTypes
{
    [TestClass]
    public class FlowValueTypeTests
    {
        [TestMethod]
        public void Given_Valid_Cadence_Json_Create_ValueType()
        {
            var cadenceJsonString = @"{""type"":""Address"",""value"":""0x66d6f450e25a4e22""}";
            var flowValueType = FlowValueType.CreateFromCadence(cadenceJsonString);
            Assert.AreEqual("Address", flowValueType.Type);
        }

        [TestMethod]
        public void CreateFromCadence_NullType_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FlowValueType.CreateFromCadence(null, "asdf"));
        }

        [TestMethod]
        public void CreateFromCadence_InvalidType_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => FlowValueType.CreateFromCadence("asdf", "asdf"));
        }

        [TestMethod]
        public void Create_OptionalType_NoNestedType_ThrowsInvalidOperationException()
        {
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                FlowValueType.Create("Optional", 123);
            });
        }

        [TestMethod]
        public void Create_NullType_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FlowValueType.Create(null, 1));
        }

        [TestMethod]
        public void Create_InvalidType_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => FlowValueType.Create("asdf", 1));
        }

        [TestMethod]
        public void Create_OptionalType_CreatesOptionalType()
        {
            var type = "Optional|Int16";

            var result = FlowValueType.Create(type, (short)123);

            //verify we got a OptionalType object back
            Assert.IsInstanceOfType(result, typeof(OptionalType));

            //get the data and verify it's int16
            var optional = result as OptionalType;
            var data = optional.Data;
            Assert.IsInstanceOfType(data, typeof(Int16Type));

            //verify value
            var int16Data = optional.Data as Int16Type;
            Assert.AreEqual(123, int16Data.Data);
        }

        [TestMethod]
        public void Create_ValidTypeInvalidValue_ThrowsInvalidOperationException()
        {
            var ex = Assert.ThrowsException<InvalidOperationException>(() => FlowValueType.Create("String", 123));

            Assert.IsNotNull(ex.InnerException);
        }

        [TestMethod]
        public void Create_AddressType_ReturnsAddressType()
        {
            var expectedData = "0x1234";
            var result = FlowValueType.Create("Address", expectedData);

            Assert.IsInstanceOfType(result, typeof(AddressType));

            var resultData = (result as AddressType).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_StringType_ReturnsStringType()
        {
            var expectedData = "hello world!";
            var result = FlowValueType.Create("String", expectedData);

            Assert.IsInstanceOfType(result, typeof(StringType));

            var resultData = (result as StringType).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UIntType_ReturnsUIntType()
        {
            uint expectedData = 5;
            var result = FlowValueType.Create("UInt", expectedData);

            Assert.IsInstanceOfType(result, typeof(UIntType));

            var resultData = (result as UIntType).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt8Type_ReturnsUInt8Type()
        {
            uint expectedData = 5;
            var result = FlowValueType.Create("UInt8", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt8Type));

            var resultData = (result as UInt8Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt16Type_ReturnsUInt16Type()
        {
            uint expectedData = 5;
            var result = FlowValueType.Create("UInt16", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt16Type));

            var resultData = (result as UInt16Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt32Type_ReturnsUInt32Type()
        {
            uint expectedData = 5;
            var result = FlowValueType.Create("UInt32", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt32Type));

            var resultData = (result as UInt32Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt64Type_ReturnsUInt64Type()
        {
            ulong expectedData = 5;
            var result = FlowValueType.Create("UInt64", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt64Type));

            var resultData = (result as UInt64Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt128Type_ReturnsUInt128Type()
        {
            BigInteger expectedData = BigInteger.Parse("7777777777777777777777777777777777777777777");
            var result = FlowValueType.Create("UInt128", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt128Type));

            var resultData = (result as UInt128Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UInt256Type_ReturnsUInt256Type()
        {
            BigInteger expectedData = BigInteger.Parse("7777777777777777777777777777777777777777777");
            var result = FlowValueType.Create("UInt256", expectedData);

            Assert.IsInstanceOfType(result, typeof(UInt256Type));

            var resultData = (result as UInt256Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_IntType_ReturnsIntType()
        {
            int expectedData = 5;
            var result = FlowValueType.Create("Int", expectedData);

            Assert.IsInstanceOfType(result, typeof(IntType));

            var resultData = (result as IntType).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int8Type_ReturnsInt8Type()
        {
            int expectedData = 5;
            var result = FlowValueType.Create("Int8", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int8Type));

            var resultData = (result as Int8Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int16Type_ReturnsInt16Type()
        {
            short expectedData = 5;
            var result = FlowValueType.Create("Int16", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int16Type));

            var resultData = (result as Int16Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int32Type_ReturnsInt32Type()
        {
            int expectedData = 5;
            var result = FlowValueType.Create("Int32", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int32Type));

            var resultData = (result as Int32Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int64Type_ReturnsInt64Type()
        {
            long expectedData = 5;
            var result = FlowValueType.Create("Int64", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int64Type));

            var resultData = (result as Int64Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int128Type_ReturnsInt128Type()
        {
            BigInteger expectedData = BigInteger.Parse("7777777777777777777777777777777777777777777");
            var result = FlowValueType.Create("Int128", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int128Type));

            var resultData = (result as Int128Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Int256Type_ReturnsInt256Type()
        {
            BigInteger expectedData = BigInteger.Parse("7777777777777777777777777777777777777777777");
            var result = FlowValueType.Create("Int256", expectedData);

            Assert.IsInstanceOfType(result, typeof(Int256Type));

            var resultData = (result as Int256Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Fix64Type_ReturnFix64Type()
        {
            decimal expectedData = 12345.789m;
            var result = FlowValueType.Create("Fix64", expectedData);

            Assert.IsInstanceOfType(result, typeof(Fix64Type));

            var resultData = (result as Fix64Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_UFix64Type_ReturnFix64Type()
        {
            decimal expectedData = 12345.789m;
            var result = FlowValueType.Create("UFix64", expectedData);

            Assert.IsInstanceOfType(result, typeof(UFix64Type));

            var resultData = (result as UFix64Type).Data;

            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_DictionaryType_ReturnsDictionaryType()
        {
            UInt8Type key = new UInt8Type(5);
            StringType value = new StringType("asdf");

            var dict = new Dictionary<FlowValueType, FlowValueType>()
            {
                { key, value }
            };

            var result = FlowValueType.Create("Dictionary", dict);

            Assert.IsInstanceOfType(result, typeof(DictionaryType));

            var resultData = (result as DictionaryType).Data;
            Assert.AreEqual(dict.Keys.Count, resultData.Count);
            Assert.IsTrue(dict.ContainsKey(key));
            Assert.IsInstanceOfType(dict[key], typeof(StringType));

            var stringResultData = (dict[key] as StringType).Data;
            Assert.AreEqual(value.Data, stringResultData);
        }

        [TestMethod]
        public void Create_DictionaryTypeCadenceJson_ReturnsDictionaryType()
        {
            var json = @"[{""key"":{""type"":""UInt8"",""value"":""123""},""value"":{""type"":""String"",""value"":""test""}}]";
            var result = FlowValueType.Create("Dictionary", json);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DictionaryType));

            var data = (result as DictionaryType).Data;
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Keys.Count);

            //verify values
            var kvp = data.First();
            var key = kvp.Key;
            Assert.IsInstanceOfType(key, typeof(UInt8Type));
            Assert.AreEqual((UInt32)123, ((UInt8Type)key).Data);

            var value = kvp.Value;
            Assert.IsInstanceOfType(value, typeof(StringType));
            Assert.AreEqual("test", ((StringType)value).Data);
        }

        [TestMethod]
        public void Create_ArrayType_ReturnsArrayType()
        {
            var value = new StringType("hello");
            var arr = new List<FlowValueType>() { value };

            var result = FlowValueType.Create("Array", arr);

            Assert.IsInstanceOfType(result, typeof(ArrayType));

            var resultData = (result as ArrayType).Data;
            Assert.AreEqual(arr.Count, resultData.Count);
            Assert.IsInstanceOfType(resultData[0], typeof(StringType));

            var item = (resultData[0] as StringType).Data;

            Assert.AreEqual(value.Data, item);
        }

        [TestMethod]
        public void Create_ArrayTypeCadenceJson_ReturnsArrayType()
        {
            var item = new Int16Type(123);
            var data = new List<FlowValueType>()
            {
                item
            };
            var expectedCadenceJson = @"{""type"":""Array"",""value"":[{""type"":""Int16"",""value"":""123""}]}";

            var result = FlowValueType.Create("Array", expectedCadenceJson);

            Assert.IsInstanceOfType(result, typeof(ArrayType));

            var resultArr = (result as ArrayType);
            Assert.AreEqual(data.Count, resultArr.Data.Count);
            Assert.IsInstanceOfType(resultArr.Data[0], typeof(Int16Type));

            var resultItem = (resultArr.Data[0] as Int16Type);
            Assert.AreEqual(item.Data, resultItem.Data);
        }

        [TestMethod]
        public void Create_BoolType_ReturnsBoolType()
        {
            bool expectedData = false;

            var result = FlowValueType.Create("Bool", expectedData);

            Assert.IsInstanceOfType(result, typeof(BoolType));

            bool resultData = (result as BoolType).Data;
            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Word8Type_ReturnsWord8Type()
        {
            uint expectedData = 5;

            var result = FlowValueType.Create("Word8", expectedData);

            Assert.IsInstanceOfType(result, typeof(Word8Type));

            var resultData = (result as Word8Type).Data;
            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Word16Type_ReturnsWord16Type()
        {
            uint expectedData = 5;

            var result = FlowValueType.Create("Word16", expectedData);

            Assert.IsInstanceOfType(result, typeof(Word16Type));

            var resultData = (result as Word16Type).Data;
            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Word32Type_ReturnsWord32Type()
        {
            uint expectedData = 5;

            var result = FlowValueType.Create("Word32", expectedData);

            Assert.IsInstanceOfType(result, typeof(Word32Type));

            var resultData = (result as Word32Type).Data;
            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_Word64Type_ReturnsWord64Type()
        {
            ulong expectedData = 5;

            var result = FlowValueType.Create("Word64", expectedData);

            Assert.IsInstanceOfType(result, typeof(Word64Type));

            var resultData = (result as Word64Type).Data;
            Assert.AreEqual(expectedData, resultData);
        }

        [TestMethod]
        public void Create_FlowType_ReturnsFlowType()
        {
            var typeJson = "{\"type\":\"Type\",\"value\":{\"staticType\":{\"kind\":\"Resource\",\"typeID\":\"A.ff68241f0f4fd521.DrSeuss.NFT\",\"fields\":[{\"id\":\"uuid\",\"type\":{\"kind\":\"UInt64\"}},{\"id\":\"id\",\"type\":{\"kind\":\"UInt64\"}},{\"id\":\"mintNumber\",\"type\":{\"kind\":\"UInt32\"}},{\"id\":\"contentCapability\",\"type\":{\"kind\":\"Capability\",\"type\":\"\"}},{\"id\":\"contentId\",\"type\":{\"kind\":\"String\"}}],\"initializers\":[],\"type\":\"\"}}}";

            var result = FlowValueType.Create("Type", typeJson);

            Assert.IsInstanceOfType(result, typeof(FlowType));

            Assert.IsNotNull(result);

            var flowType = result as FlowType;
            Assert.AreEqual("Type", flowType.Type);

            var data = flowType.Data;
            Assert.IsNotNull(data);

            Assert.IsInstanceOfType(data, typeof(CompositeTypeDefinition));
        }

        [TestMethod]
        public void Create_PathType_ReturnsPathType()
        {
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                {"domain", "storage"},
                {"identifier", "flowTokenVault"}
            };
            var result = FlowValueType.Create("Path", values);

            Assert.IsInstanceOfType(result, typeof(PathType));

            var path = result as PathType;

            Assert.AreEqual("storage", path.Domain);
            Assert.AreEqual("flowTokenVault", path.Identifier);
        }

        [TestMethod]
        public void Create_CapabilityType_ReturnsCapabilityType()
        {
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                {"path", "/public/someInteger"},
                {"address", "0x1"},
                {"borrowType", "Int"}
            };

            var result = FlowValueType.Create("Capability", values);

            Assert.IsInstanceOfType(result, typeof(CapabilityType));

            var cap = result as CapabilityType;

            Assert.AreEqual("/public/someInteger", cap.Path);
            Assert.AreEqual("0x1", cap.Address);
            Assert.AreEqual("Int", cap.BorrowType);
        }

        [TestMethod]
        [DataRow("Address", true)]
        [DataRow("String", true)]
        [DataRow("Bool", true)]
        [DataRow("UInt", true)]
        [DataRow("UInt8", true)]
        [DataRow("UInt16", true)]
        [DataRow("UInt32", true)]
        [DataRow("UInt64", true)]
        [DataRow("UInt128", true)]
        [DataRow("UInt256", true)]
        [DataRow("Int", true)]
        [DataRow("Int8", true)]
        [DataRow("Int16", true)]
        [DataRow("Int32", true)]
        [DataRow("Int64", true)]
        [DataRow("Int128", true)]
        [DataRow("Int256", true)]
        [DataRow("UFix64", true)]
        [DataRow("Fix64", true)]
        [DataRow("Word8", true)]
        [DataRow("Word16", true)]
        [DataRow("Word32", true)]
        [DataRow("Word64", true)]
        [DataRow("Optional", false)] //complex types
        [DataRow("Path", false)]
        [DataRow("Capability", false)]
        [DataRow("Dictionary", false)]
        [DataRow("Array", false)]
        [DataRow("Type", false)]
        [DataRow("Struct", false)]
        [DataRow("Resource", false)]
        [DataRow("Event", false)]
        [DataRow("Contract", false)]
        [DataRow("Enum", false)]
        public void IsPrimitiveType_Test(string type, bool expectedValue)
        {
            bool res = FlowValueType.IsPrimitiveType(type);

            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void DictionaryWithNestedStructs_CreateFromCadence_ReturnsDictionaryType()
        {
            var json = "{\"type\":\"Dictionary\",\"value\":[{\"key\":{\"type\":\"String\",\"value\":\"itemVideo\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"https://leela.mypinata.cloud/ipfs/QmeirDvh3TrDgtDdfvyjQXF87DusXksymtzmA4RABBVreo/LOOP.mp4\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":true}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"secondaryColor\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"545454\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":false}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"primaryColor\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"FF803E\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":false}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"itemImage4\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":true}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"itemImage3\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":true}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"itemImage2\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":true}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"season\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"1\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":false}}]}}},{\"key\":{\"type\":\"String\",\"value\":\"itemImage\"},\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.2a37a78609bba037.TheFabricantS1ItemNFT.Metadata\",\"fields\":[{\"name\":\"metadataValue\",\"value\":{\"type\":\"String\",\"value\":\"https://leela.mypinata.cloud/ipfs/QmeirDvh3TrDgtDdfvyjQXF87DusXksymtzmA4RABBVreo/LOOP_poster.png\"}},{\"name\":\"mutable\",\"value\":{\"type\":\"Bool\",\"value\":true}}]}}}]}";

            var result = FlowValueType.CreateFromCadence("Dictionary", json);
            Assert.IsNotNull(result);

            var dict = result as DictionaryType;
            var data = dict.Data;

            foreach (var kvp in data)
            {
                var key = kvp.Key;
                var value = kvp.Value;

                Assert.AreEqual("String", key.Type);
                Assert.AreEqual("Struct", value.Type);

                Assert.IsInstanceOfType(value, typeof(CompositeType));

                var compositeType = value as CompositeType;
                var compositeData = compositeType.Data;

                //just verify the struct has fields
                //we can rely on tests for CompositeType for actual field parsing
                Assert.IsNotNull(compositeData);
                Assert.IsTrue(compositeData.Fields.Any());
                Assert.IsFalse(string.IsNullOrEmpty(compositeData.Id));
            }
        }

        [TestMethod]
        [DataRow("Struct")]
        [DataRow("Resource")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("Enum")]
        public void Create_CompositeType_ReturnsCompositeType(string type)
        {
            var id = "structId";

            var intType = new Int16Type(123);
            var stringType = new StringType("asdf");

            var fields = new List<CompositeField>()
            {
                new CompositeField("intTypeId", intType),
                new CompositeField("stringTypeId", stringType)
            };

            var value = new CompositeData(id, fields);

            var result = FlowValueType.Create(type, value);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CompositeType));

            var composite = result as CompositeType;

            Assert.AreEqual(type, composite.Type);
            Assert.AreEqual(id, composite.Id);

            var resultFields = composite.Fields;
            Assert.IsNotNull(resultFields);
            Assert.AreEqual(fields.Count, resultFields.Count);

            //verify individual fields
            var firstField = resultFields[0];
            Assert.AreEqual("intTypeId", firstField.Name);
            var firstValue = firstField.Value;
            Assert.IsNotNull(firstValue);
            Assert.IsInstanceOfType(firstValue, typeof(Int16Type));
            var resultIntType = firstValue as Int16Type;
            Assert.AreEqual(intType.Data, resultIntType.Data);

            var secondField = resultFields[1];
            Assert.AreEqual("stringTypeId", secondField.Name);
            var secondValue = secondField.Value;
            Assert.IsNotNull(secondValue);
            Assert.IsInstanceOfType(secondValue, typeof(StringType));
            var resultStringType = secondValue as StringType;
            Assert.AreEqual(intType.Data, resultIntType.Data);
        }

        [TestMethod]
        [DataRow("Struct")]
        [DataRow("Resource")]
        [DataRow("Event")]
        [DataRow("Contract")]
        [DataRow("Enum")]
        public void CreateFromCadence_CompositeType_ReturnsCompositeType(string type)
        {
            var json = $"{{\"type\":\"{type}\",\"value\":{{\"fields\":[{{\"name\":\"intField\",\"value\":{{\"type\":\"Int16\",\"value\":\"123\"}}}},{{\"name\":\"stringField\",\"value\":{{\"type\":\"String\",\"value\":\"hello\"}}}}],\"id\":\"idString\"}}}}";

            var result = FlowValueType.CreateFromCadence(json);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CompositeType));

            var composite = result as CompositeType;
            Assert.AreEqual(type, composite.Type);

            var data = composite.Data;
            Assert.IsNotNull(data);

            var id = data.Id;
            Assert.AreEqual("idString", id);

            var fields = data.Fields;
            Assert.IsNotNull(fields);
            Assert.AreEqual(2, fields.Count);

            //verify data
            var firstName = fields[0].Name;
            Assert.AreEqual("intField", firstName);

            var firstValue = fields[0].Value;
            Assert.IsNotNull(firstValue);
            Assert.IsInstanceOfType(firstValue, typeof(Int16Type));

            var intType = firstValue as Int16Type;
            Assert.AreEqual(123, intType.Data);

            var secondName = fields[1].Name;
            Assert.AreEqual("stringField", secondName);

            var secondValue = fields[1].Value;
            Assert.IsNotNull(secondValue);
            Assert.IsInstanceOfType(secondValue, typeof(StringType));

            var stringType = secondValue as StringType;
            Assert.AreEqual("hello", stringType.Data);
        }

        [TestMethod]
        [DataRow("Address", false)]
        [DataRow("String", false)]
        [DataRow("Bool", false)]
        [DataRow("UInt", false)]
        [DataRow("UInt8", false)]
        [DataRow("UInt16", false)]
        [DataRow("UInt32", false)]
        [DataRow("UInt64", false)]
        [DataRow("UInt128", false)]
        [DataRow("UInt256", false)]
        [DataRow("Int", false)]
        [DataRow("Int8", false)]
        [DataRow("Int16", false)]
        [DataRow("Int32", false)]
        [DataRow("Int64", false)]
        [DataRow("Int128", false)]
        [DataRow("Int256", false)]
        [DataRow("UFix64", false)]
        [DataRow("Fix64", false)]
        [DataRow("Word8", false)]
        [DataRow("Word16", false)]
        [DataRow("Word32", false)]
        [DataRow("Word64", false)]
        [DataRow("Optional", false)]
        [DataRow("Path", false)]
        [DataRow("Capability", false)]
        [DataRow("Dictionary", false)]
        [DataRow("Array", false)]
        [DataRow("Type", false)]
        [DataRow("Struct", true)]
        [DataRow("Resource", true)]
        [DataRow("Event", true)]
        [DataRow("Contract", true)]
        [DataRow("Enum", true)]
        public void IsCompositeType_ReturnsCorrectValue(string type, bool expectedResult)
        {
            var result = FlowValueType.IsCompositeType(type);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void CreateFromCadence_OptionalStruct_NestedStructs_With_NestedArray_ReturnsOptionalType()
        {
            //see testnet transaction 94c061a2075679cf8df22bab85f2979739921a0c64939ce7ae1036629b55eaff
            //note: as of writing this comment flowscan is unable to parse this contract
            var json = "{\"type\":\"Optional\",\"value\":{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeMain.MainDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"4355\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Fengfeng\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Fengfeng is a young lady. and she has a lot of strange friends since she was a little girl. Someone say they know Fengfeng. and she is reticent and weird. some find her confident and charming. and some others say she's a whore and bitch. See. how big the difference is. stop judging others. okay?\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"\"}},{\"name\":\"componentDetails\",\"value\":{\"type\":\"Array\",\"value\":[{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"107520\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Victorian Dream\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Elegant victorian fancy Lace gloves. Sexy and chic. Well complement Bella's little dress.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Hand Accessories\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"12\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"321\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmW54UNsWyz1gZ1AnMpV1x6iFrAdqQejykTDLwisTecELD\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"108841\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Silk Reflection\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Lovely lace detailing makes these sheer thigh highs even more alluring.  \"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Leg Accessories\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"3\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"342\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmPeqTbUXMWy4gYnJSJJ8rKEYkuuj9zzZFRVUBWinNmpai\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"101055\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Fengfeng\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Fengfeng is a young lady. and she has a lot of strange friends since she was a little girl. Someone say they know Fengfeng. and she is reticent and weird. some find her confident and charming. and some others say she's a whore and bitch. See. how big the difference is. stop judging others. okay?\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Body\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"2\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"356\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"2500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmeXGfuHMtGLNQnqavwYzQJ2EiNTddKiJX3cGW6vnxbyQv\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"116193\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Summer Time \"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Sunshine. flowers. coconut juice. everything is fantasy like land of dreams.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Background\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"1\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"1244\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"1250\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmTagXTW3exGJk1BZ7jae6G3SQ49N8YFnkoQF15Mb9Sw6j\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"96590\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Braveheart\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"We will end up dead. and only a brave heart can lead you forward. \"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Makeup\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"7\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"491\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmP8QqHVcU63ij8mkrtPDNgQLk37Z8oD3oeJvXR3VBLdkN\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"110501\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Late Night Party \"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"A sexy dress and sparkling makeup add true glamour to you tonight.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Clothes\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"4\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"302\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmVcTKWZnHxcKzf4BU8GQReWaW1kKv1TmoNvUeJ79skAuj\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"112368\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Focus\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Keep spinning. you'll draw everyone's attention in the prom.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Shoes\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"6\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"269\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmdBj1LKucuWDN6qDBD2sqfaY5BEb5TTyhYHbiQtaRKpM5\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"106130\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Pearl Drop Earrings\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"With a simple enough design. these classic earrings are a staple for elegant ladies like Bella. \"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Ear Accessories\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"10\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"431\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmawHpLtWzQ5VWD2wfA7abGdn5xb4qCbasq7MHsqfUziVf\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"98943\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Young Idol\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Peacock green dip-dye hair highlights. no one can miss that design.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Hair\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"8\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"244\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmdTKpqYbZKkRjbL9MeBeYsSEKpNHmbrPF61kaoDcphV8f\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"104012\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Pearl Headband\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"Elegant headband features shiny pearls for a timeless look. Good choice for a prom.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Head Accessories \"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"9\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"313\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmerxXw1rV55ivfpDgRndD31frQcDXLzsQBHpW8hGu9Ddv\"}}]}},{\"type\":\"Struct\",\"value\":{\"id\":\"A.b4187e54e0ed55a8.SoulMadeComponent.ComponentDetail\",\"fields\":[{\"name\":\"id\",\"value\":{\"type\":\"UInt64\",\"value\":\"106820\"}},{\"name\":\"series\",\"value\":{\"type\":\"String\",\"value\":\"Disordered-FengFeng\"}},{\"name\":\"name\",\"value\":{\"type\":\"String\",\"value\":\"Pearl Queen\"}},{\"name\":\"description\",\"value\":{\"type\":\"String\",\"value\":\"A beautiful necklace made with hand picked pearls. At a glance. it is stunning. Up close. it is gorgeous. Don't forget to wear a pair of pearl earrings.\"}},{\"name\":\"category\",\"value\":{\"type\":\"String\",\"value\":\"Neck Accessories\"}},{\"name\":\"layer\",\"value\":{\"type\":\"UInt64\",\"value\":\"11\"}},{\"name\":\"edition\",\"value\":{\"type\":\"UInt64\",\"value\":\"121\"}},{\"name\":\"maxEdition\",\"value\":{\"type\":\"UInt64\",\"value\":\"500\"}},{\"name\":\"ipfsHash\",\"value\":{\"type\":\"String\",\"value\":\"QmWhfNS9WurU1N376dGgPjvwtadPcA3Vm9Bokseus3nyhd\"}}]}}]}}]}}}";

            var result = FlowValueType.CreateFromCadence("Optional", json);

            Assert.IsInstanceOfType(result, typeof(OptionalType));

            var optional = result as OptionalType;

            Assert.IsInstanceOfType(optional.Data, typeof(CompositeType));

            var composite = optional.Data as CompositeType;
            Assert.AreEqual("Struct", composite.Type);
            Assert.AreEqual("A.b4187e54e0ed55a8.SoulMadeMain.MainDetail", composite.Id);

            //this json  is massive cant really verify every property just make sure there are fields
            Assert.IsNotNull(composite.Fields);
            Assert.IsTrue(composite.Fields.Any());
        }

        [TestMethod]
        public void CreateFromCadence_FunctionType()
        {
            var json = @"{""type"":""Function"",""value"":{""functionType"":{""kind"":""Function"",""typeID"":""(():Void)"",""parameters"":[],""return"":{""kind"":""Void""}}}}";
            var res = FlowValueType.CreateFromCadence("Function", json);
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(FunctionType));

            var res2 = FlowValueType.CreateFromCadence(json);
            Assert.IsInstanceOfType(res2, typeof(FunctionType));
        }

        [TestMethod]
        public void Create_FunctionType()
        {
            var json = @"{""type"":""Function"",""value"":{""functionType"":{""kind"":""Function"",""typeID"":""(():Void)"",""parameters"":[],""return"":{""kind"":""Void""}}}}";
            var res = FlowValueType.Create("Function", json);
            Assert.IsInstanceOfType(res, typeof(FunctionType));
        }
    }
}

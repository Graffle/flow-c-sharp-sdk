using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Graffle.FlowSdk.Types;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var expectedData = "A.ca4ee530dafff8ad.Evolution.NFT";

            var result = FlowValueType.Create("Type", expectedData);

            Assert.IsInstanceOfType(result, typeof(FlowType));

            var resultData = (result as FlowType).Data;
            Assert.AreEqual("A.ca4ee530dafff8ad.Evolution.NFT", resultData);
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
    }
}

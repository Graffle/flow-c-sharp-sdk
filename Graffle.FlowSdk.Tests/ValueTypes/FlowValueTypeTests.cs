using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Graffle.FlowSdk.Types;
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
            Assert.AreEqual(flowValueType.Type, "Address");
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
            var expectedData = "hello";

            var result = FlowValueType.Create("Type", expectedData);

            Assert.IsInstanceOfType(result, typeof(FlowType));

            var resultData = (result as FlowType).Data;
            Assert.AreEqual(expectedData, resultData);
        }
    }
}

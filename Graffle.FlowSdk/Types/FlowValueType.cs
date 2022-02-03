using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Protobuf;

namespace Graffle.FlowSdk.Types
{
    public abstract class FlowValueType
    {
        protected const string ADDRESS_TYPE_NAME = "Address";

        protected const string OPTIONAL_TYPE_NAME = "Optional";

        protected const string PATH_TYPE_NAME = "Path";
        protected const string CAPABILITY_TYPE_NAME = "Capability";

        protected const string STRING_TYPE_NAME = "String";

        protected const string BOOL_TYPE_NAME = "Bool";

        protected const string UINT_TYPE_NAME = "UInt";
        protected const string UINT8_TYPE_NAME = "UInt8";
        protected const string UINT16_TYPE_NAME = "UInt16";
        protected const string UINT32_TYPE_NAME = "UInt32";
        protected const string UINT64_TYPE_NAME = "UInt64";
        protected const string UINT128_TYPE_NAME = "UInt128";
        protected const string UINT256_TYPE_NAME = "UInt256";

        protected const string INT_TYPE_NAME = "Int";
        protected const string INT8_TYPE_NAME = "Int8";
        protected const string INT16_TYPE_NAME = "Int16";
        protected const string INT32_TYPE_NAME = "Int32";
        protected const string INT64_TYPE_NAME = "Int64";
        protected const string INT128_TYPE_NAME = "Int128";
        protected const string INT256_TYPE_NAME = "Int256";

        protected const string UFIX64_TYPE_NAME = "UFix64";
        protected const string FIX64_TYPE_NAME = "Fix64";

        protected const string DICTIONARY_TYPE_NAME = "Dictionary";
        protected const string ARRAY_TYPE_NAME = "Array";

        protected const string FLOW_TYPE_NAME = "Type";

        protected const string WORD8_TYPE_NAME = "Word8";
        protected const string WORD16_TYPE_NAME = "Word16";
        protected const string WORD32_TYPE_NAME = "Word32";
        protected const string WORD64_TYPE_NAME = "Word64";


        [JsonPropertyName("type")]
        public abstract string Type { get; }
        public abstract string AsJsonCadenceDataFormat();
        public abstract string DataAsJson();

        public ByteString ToByteString()
        {
            var format = this.AsJsonCadenceDataFormat();
            var bytes = Encoding.ASCII.GetBytes(format);
            return ByteString.CopyFrom(bytes);
        }

        public static FlowValueType CreateFromCadence(string cadenceJsonValue)
        {
            var parsedJson = JsonDocument.Parse(cadenceJsonValue);
            var type = parsedJson.RootElement.GetProperty("type").ToString();
            return CreateFromCadence(type, cadenceJsonValue);
        }

        public static FlowValueType CreateFromCadence(string type, string cadenceJsonValue)
        {
            return type switch
            {
                ADDRESS_TYPE_NAME => AddressType.FromJson(cadenceJsonValue),
                OPTIONAL_TYPE_NAME => OptionalType.FromJson(cadenceJsonValue),
                STRING_TYPE_NAME => StringType.FromJson(cadenceJsonValue),
                UINT_TYPE_NAME => UIntType.FromJson(cadenceJsonValue),
                UINT8_TYPE_NAME => UInt8Type.FromJson(cadenceJsonValue),
                UINT16_TYPE_NAME => UInt16Type.FromJson(cadenceJsonValue),
                UINT32_TYPE_NAME => UInt32Type.FromJson(cadenceJsonValue),
                UINT64_TYPE_NAME => UInt64Type.FromJson(cadenceJsonValue),
                UINT128_TYPE_NAME => UInt128Type.FromJson(cadenceJsonValue),
                UINT256_TYPE_NAME => UInt256Type.FromJson(cadenceJsonValue),
                INT_TYPE_NAME => IntType.FromJson(cadenceJsonValue),
                INT8_TYPE_NAME => Int8Type.FromJson(cadenceJsonValue),
                INT16_TYPE_NAME => Int16Type.FromJson(cadenceJsonValue),
                INT32_TYPE_NAME => Int32Type.FromJson(cadenceJsonValue),
                INT64_TYPE_NAME => Int64Type.FromJson(cadenceJsonValue),
                INT128_TYPE_NAME => Int128Type.FromJson(cadenceJsonValue),
                INT256_TYPE_NAME => Int256Type.FromJson(cadenceJsonValue),
                FIX64_TYPE_NAME => Fix64Type.FromJson(cadenceJsonValue),
                UFIX64_TYPE_NAME => UFix64Type.FromJson(cadenceJsonValue),
                DICTIONARY_TYPE_NAME => DictionaryType.FromJson(cadenceJsonValue),
                ARRAY_TYPE_NAME => ArrayType.FromJson(cadenceJsonValue),
                BOOL_TYPE_NAME => BoolType.FromJson(cadenceJsonValue),
                FLOW_TYPE_NAME => FlowType.FromJson(cadenceJsonValue),
                WORD8_TYPE_NAME => Word8Type.FromJson(cadenceJsonValue),
                WORD16_TYPE_NAME => Word16Type.FromJson(cadenceJsonValue),
                WORD32_TYPE_NAME => Word32Type.FromJson(cadenceJsonValue),
                WORD64_TYPE_NAME => Word64Type.FromJson(cadenceJsonValue),
                PATH_TYPE_NAME => PathType.FromJson(cadenceJsonValue),
                CAPABILITY_TYPE_NAME => PathType.FromJson(cadenceJsonValue),
                _ => throw new ArgumentException($"Flow Value Type of {type} does not exist.")
            };
        }

        public static FlowValueType Create(string type, dynamic value)
        {
            var splitValues = type.Split('|');
            return splitValues.First() switch
            {
                ADDRESS_TYPE_NAME => new AddressType(value),
                OPTIONAL_TYPE_NAME => new OptionalType(FlowValueType.Create(splitValues.Last(), value)),
                STRING_TYPE_NAME => new StringType(value),
                UINT_TYPE_NAME => new UIntType(value),
                UINT8_TYPE_NAME => new UInt8Type(value),
                UINT16_TYPE_NAME => new UInt16Type(value),
                UINT32_TYPE_NAME => new UInt32Type(value),
                UINT64_TYPE_NAME => new UInt64Type(value),
                UINT128_TYPE_NAME => new UInt128Type(value),
                UINT256_TYPE_NAME => new UInt256Type(value),
                INT_TYPE_NAME => new IntType(value),
                INT8_TYPE_NAME => new Int8Type(value),
                INT16_TYPE_NAME => new Int16Type(value),
                INT32_TYPE_NAME => new Int32Type(value),
                INT64_TYPE_NAME => new Int64Type(value),
                INT128_TYPE_NAME => new Int128Type(value),
                INT256_TYPE_NAME => new Int256Type(value),
                FIX64_TYPE_NAME => new Fix64Type(value),
                UFIX64_TYPE_NAME => new UFix64Type(value),
                DICTIONARY_TYPE_NAME => new DictionaryType(value),
                ARRAY_TYPE_NAME => value is string ? ArrayType.CreateFromCadence(type, value) : new ArrayType(value),
                BOOL_TYPE_NAME => new BoolType(value),
                FLOW_TYPE_NAME => new FlowType(value),
                WORD8_TYPE_NAME => new Word8Type(value),
                WORD16_TYPE_NAME => new Word16Type(value),
                WORD32_TYPE_NAME => new Word32Type(value),
                WORD64_TYPE_NAME => new Word64Type(value),
                _ => throw new ArgumentException($"Flow Value Type of {type} does not exist.")
            };
        }

    }

    public abstract class FlowValueType<T> : FlowValueType
    {

        protected FlowValueType(T data)
        {
            Data = data;
        }

        [JsonPropertyName("data")]
        public virtual T Data { get; }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
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
        private delegate FlowValueType FromCadenceJson(string json);
        private static readonly Dictionary<string, FromCadenceJson> typeNameToConverter;

        static FlowValueType()
        {
            typeNameToConverter = new Dictionary<string, FromCadenceJson>()
            {
                { Constants.ADDRESS_TYPE_NAME, AddressType.FromJson },
                { Constants.OPTIONAL_TYPE_NAME , OptionalType.FromJson },
                { Constants.STRING_TYPE_NAME , StringType.FromJson },
                { Constants.UINT_TYPE_NAME , UIntType.FromJson },
                { Constants.UINT8_TYPE_NAME , UInt8Type.FromJson },
                { Constants.UINT16_TYPE_NAME , UInt16Type.FromJson },
                { Constants.UINT32_TYPE_NAME , UInt32Type.FromJson },
                { Constants.UINT64_TYPE_NAME , UInt64Type.FromJson },
                { Constants.UINT128_TYPE_NAME , UInt128Type.FromJson },
                { Constants.UINT256_TYPE_NAME , UInt256Type.FromJson },
                { Constants.INT_TYPE_NAME , IntType.FromJson },
                { Constants.INT8_TYPE_NAME , Int8Type.FromJson },
                { Constants.INT16_TYPE_NAME , Int16Type.FromJson },
                { Constants.INT32_TYPE_NAME , Int32Type.FromJson },
                { Constants.INT64_TYPE_NAME , Int64Type.FromJson },
                { Constants.INT128_TYPE_NAME , Int128Type.FromJson },
                { Constants.INT256_TYPE_NAME , Int256Type.FromJson },
                { Constants.FIX64_TYPE_NAME , Fix64Type.FromJson },
                { Constants.UFIX64_TYPE_NAME , UFix64Type.FromJson },
                { Constants.DICTIONARY_TYPE_NAME , DictionaryType.FromJson },
                { Constants.ARRAY_TYPE_NAME , ArrayType.FromJson },
                { Constants.BOOL_TYPE_NAME , BoolType.FromJson },
                { Constants.FLOW_TYPE_NAME , FlowType.FromJson },
                { Constants.WORD8_TYPE_NAME , Word8Type.FromJson },
                { Constants.WORD16_TYPE_NAME , Word16Type.FromJson },
                { Constants.WORD32_TYPE_NAME , Word32Type.FromJson },
                { Constants.WORD64_TYPE_NAME , Word64Type.FromJson },
                { Constants.PATH_TYPE_NAME , PathType.FromJson },
                { Constants.CAPABILITY_TYPE_NAME , PathType.FromJson },
            };
        }

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
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!typeNameToConverter.ContainsKey(type))
                throw new ArgumentException($"Flow Value Type of {type} does not exist.", nameof(type));

            return typeNameToConverter[type](cadenceJsonValue);
        }

        public static FlowValueType Create(string type, dynamic value)
        {
            var splitValues = type.Split('|');
            return splitValues.First() switch
            {
                Constants.ADDRESS_TYPE_NAME => new AddressType(value),
                Constants.OPTIONAL_TYPE_NAME => new OptionalType(FlowValueType.Create(splitValues.Last(), value)),
                Constants.STRING_TYPE_NAME => new StringType(value),
                Constants.UINT_TYPE_NAME => new UIntType(value),
                Constants.UINT8_TYPE_NAME => new UInt8Type(value),
                Constants.UINT16_TYPE_NAME => new UInt16Type(value),
                Constants.UINT32_TYPE_NAME => new UInt32Type(value),
                Constants.UINT64_TYPE_NAME => new UInt64Type(value),
                Constants.UINT128_TYPE_NAME => new UInt128Type(value),
                Constants.UINT256_TYPE_NAME => new UInt256Type(value),
                Constants.INT_TYPE_NAME => new IntType(value),
                Constants.INT8_TYPE_NAME => new Int8Type(value),
                Constants.INT16_TYPE_NAME => new Int16Type(value),
                Constants.INT32_TYPE_NAME => new Int32Type(value),
                Constants.INT64_TYPE_NAME => new Int64Type(value),
                Constants.INT128_TYPE_NAME => new Int128Type(value),
                Constants.INT256_TYPE_NAME => new Int256Type(value),
                Constants.FIX64_TYPE_NAME => new Fix64Type(value),
                Constants.UFIX64_TYPE_NAME => new UFix64Type(value),
                Constants.DICTIONARY_TYPE_NAME => new DictionaryType(value),
                Constants.ARRAY_TYPE_NAME => value is string ? ArrayType.CreateFromCadence(type, value) : new ArrayType(value),
                Constants.BOOL_TYPE_NAME => new BoolType(value),
                Constants.FLOW_TYPE_NAME => new FlowType(value),
                Constants.WORD8_TYPE_NAME => new Word8Type(value),
                Constants.WORD16_TYPE_NAME => new Word16Type(value),
                Constants.WORD32_TYPE_NAME => new Word32Type(value),
                Constants.WORD64_TYPE_NAME => new Word64Type(value),
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
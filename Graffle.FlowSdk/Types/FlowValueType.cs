using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        private delegate FlowValueType FlowValueTypeConstructor(dynamic value);
        private static readonly Dictionary<string, FromCadenceJson> typeNameToJson;
        private static readonly Dictionary<string, FlowValueTypeConstructor> typeNameToCtor;
        private static readonly HashSet<string> primitiveTypes;

        static FlowValueType()
        {
            typeNameToJson = new Dictionary<string, FromCadenceJson>()
            {
                { Constants.ADDRESS_TYPE_NAME, AddressType.FromJson },
                { Constants.OPTIONAL_TYPE_NAME, OptionalType.FromJson },
                { Constants.STRING_TYPE_NAME, StringType.FromJson },
                { Constants.UINT_TYPE_NAME, UIntType.FromJson },
                { Constants.UINT8_TYPE_NAME, UInt8Type.FromJson },
                { Constants.UINT16_TYPE_NAME, UInt16Type.FromJson },
                { Constants.UINT32_TYPE_NAME, UInt32Type.FromJson },
                { Constants.UINT64_TYPE_NAME, UInt64Type.FromJson },
                { Constants.UINT128_TYPE_NAME, UInt128Type.FromJson },
                { Constants.UINT256_TYPE_NAME, UInt256Type.FromJson },
                { Constants.INT_TYPE_NAME, IntType.FromJson },
                { Constants.INT8_TYPE_NAME, Int8Type.FromJson },
                { Constants.INT16_TYPE_NAME, Int16Type.FromJson },
                { Constants.INT32_TYPE_NAME, Int32Type.FromJson },
                { Constants.INT64_TYPE_NAME, Int64Type.FromJson },
                { Constants.INT128_TYPE_NAME, Int128Type.FromJson },
                { Constants.INT256_TYPE_NAME, Int256Type.FromJson },
                { Constants.FIX64_TYPE_NAME, Fix64Type.FromJson },
                { Constants.UFIX64_TYPE_NAME, UFix64Type.FromJson },
                { Constants.DICTIONARY_TYPE_NAME, DictionaryType.FromJson },
                { Constants.ARRAY_TYPE_NAME, ArrayType.FromJson },
                { Constants.BOOL_TYPE_NAME, BoolType.FromJson },
                { Constants.FLOW_TYPE_NAME, FlowType.FromJson },
                { Constants.WORD8_TYPE_NAME, Word8Type.FromJson },
                { Constants.WORD16_TYPE_NAME, Word16Type.FromJson },
                { Constants.WORD32_TYPE_NAME, Word32Type.FromJson },
                { Constants.WORD64_TYPE_NAME, Word64Type.FromJson },
                { Constants.PATH_TYPE_NAME, PathType.FromJson },
                { Constants.CAPABILITY_TYPE_NAME, CapabilityType.FromJson },
            };

            typeNameToCtor = new Dictionary<string, FlowValueTypeConstructor>()
            {
                { Constants.ADDRESS_TYPE_NAME, (arg) => new AddressType(arg)},
                { Constants.STRING_TYPE_NAME, (arg) => new StringType(arg) },
                { Constants.UINT_TYPE_NAME, (arg) => new UIntType(arg) },
                { Constants.UINT8_TYPE_NAME, (arg) => new UInt8Type(arg) },
                { Constants.UINT16_TYPE_NAME, (arg) => new UInt16Type(arg) },
                { Constants.UINT32_TYPE_NAME, (arg) => new UInt32Type(arg) },
                { Constants.UINT64_TYPE_NAME, (arg) => new UInt64Type(arg) },
                { Constants.UINT128_TYPE_NAME, (arg) => new UInt128Type(arg) },
                { Constants.UINT256_TYPE_NAME, (arg) => new UInt256Type(arg) },
                { Constants.INT_TYPE_NAME, (arg) => new IntType(arg) },
                { Constants.INT8_TYPE_NAME, (arg) => new Int8Type(arg) },
                { Constants.INT16_TYPE_NAME, (arg) => new Int16Type(arg) },
                { Constants.INT32_TYPE_NAME, (arg) => new Int32Type(arg) },
                { Constants.INT64_TYPE_NAME, (arg) => new Int64Type(arg) },
                { Constants.INT128_TYPE_NAME, (arg) => new Int128Type(arg) },
                { Constants.INT256_TYPE_NAME, (arg) => new Int256Type(arg) },
                { Constants.FIX64_TYPE_NAME, (arg) => new Fix64Type(arg) },
                { Constants.UFIX64_TYPE_NAME, (arg) => new UFix64Type(arg) },
                { Constants.DICTIONARY_TYPE_NAME, (arg) => new DictionaryType(arg) },
                { Constants.ARRAY_TYPE_NAME, (arg) => arg is string ? ArrayType.CreateFromCadence(Constants.ARRAY_TYPE_NAME, arg) : new ArrayType(arg) },
                { Constants.BOOL_TYPE_NAME, (arg) => new BoolType(arg) },
                { Constants.FLOW_TYPE_NAME, (arg) => new FlowType(arg) },
                { Constants.WORD8_TYPE_NAME, (arg) => new Word8Type(arg) },
                { Constants.WORD16_TYPE_NAME, (arg) => new Word16Type(arg) },
                { Constants.WORD32_TYPE_NAME, (arg) => new Word32Type(arg) },
                { Constants.WORD64_TYPE_NAME, (arg) => new Word64Type(arg) },
            };

            primitiveTypes = new HashSet<string>()
            {
                Constants.ADDRESS_TYPE_NAME,
                Constants.STRING_TYPE_NAME,
                Constants.UINT_TYPE_NAME,
                Constants.UINT8_TYPE_NAME,
                Constants.UINT16_TYPE_NAME,
                Constants.UINT32_TYPE_NAME,
                Constants.UINT64_TYPE_NAME,
                Constants.UINT128_TYPE_NAME,
                Constants.UINT256_TYPE_NAME,
                Constants.INT_TYPE_NAME,
                Constants.INT8_TYPE_NAME,
                Constants.INT16_TYPE_NAME,
                Constants.INT32_TYPE_NAME,
                Constants.INT64_TYPE_NAME,
                Constants.INT128_TYPE_NAME,
                Constants.INT256_TYPE_NAME,
                Constants.FIX64_TYPE_NAME,
                Constants.UFIX64_TYPE_NAME,
                Constants.BOOL_TYPE_NAME,
                Constants.WORD8_TYPE_NAME,
                Constants.WORD16_TYPE_NAME,
                Constants.WORD32_TYPE_NAME,
                Constants.WORD64_TYPE_NAME,
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
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            if (typeNameToJson.TryGetValue(type, out var func))
            {
                return func(cadenceJsonValue);
            }
            else
            {
                throw new ArgumentException($"Flow Value Type of {type} does not exist.", nameof(type));
            }
        }

        public static FlowValueType Create(string type, dynamic value)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            var splitValues = type.Split('|');
            string typeToResolve = splitValues.First();
            if (typeToResolve == Constants.OPTIONAL_TYPE_NAME)
            {
                return new OptionalType(Create(splitValues.Last(), value));
            }
            else if (typeToResolve == Constants.PATH_TYPE_NAME || typeToResolve == Constants.CAPABILITY_TYPE_NAME)
            {
                //the value node for these types is json with additional child elements
                //use the typed class to parse it
                var func = typeNameToJson[typeToResolve];
                return func(value);
            }

            if (typeNameToCtor.TryGetValue(typeToResolve, out var ctor))
            {
                try
                {
                    return ctor(value);
                }
                catch (Exception ex)
                {
                    //If we're going to fail here it will probably be because value's type doesnt match the FlowType's constructor
                    //Wrap this exception so it makes more sense
                    throw new InvalidOperationException($"Failed to create Flow Value of type {type} ({typeToResolve}) with value {value}", ex);
                }
            }
            else
            {
                throw new ArgumentException($"Flow Value Type of {type} ({typeToResolve}) does not exist.", nameof(type));
            }
        }

        public static bool IsPrimitiveType(string type) => primitiveTypes.Contains(type);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    /// <summary>
    /// Base class for Type Definitions
    /// https://docs.onflow.org/cadence/json-cadence-spec/#type-value
    /// </summary>
    public abstract class TypeDefinition
    {
        [JsonPropertyName("kind")]
        public abstract string Kind { get; }

        public abstract string AsJsonCadenceDataFormat();

        /// <summary>
        /// Flattens the Type Definition and all of its properties into a dictionary of primitive types
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, dynamic> Flatten();

        public static TypeDefinition FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString());

            var kind = root.FirstOrDefault(x => x.Key == "kind").Value.ToString();
            switch (kind)
            {
                //composite types
                case "Resource":
                case "Struct":
                case "Event":
                case "Contract":
                case "StructInterface":
                case "ResourceInterface":
                case "ContractInterface":
                    return new CompositeTypeDefinition(kind, root);
                case "Capability":
                    var innerTypeJson = root.FirstOrDefault(x => x.Key == "type").Value.ToString();
                    var innerType = innerTypeJson == string.Empty ? null : TypeDefinition.FromJson(innerTypeJson);
                    return new CapabilityTypeDefinition(kind, innerType);
                case "Dictionary":
                    var key = TypeDefinition.FromJson(root["key"]);
                    var value = TypeDefinition.FromJson(root["value"]);

                    return new DictionaryTypeDefinition(key, value);
                //simple types https://docs.onflow.org/cadence/json-cadence-spec/#simple-types
                case "Int":
                case "Int8":
                case "Int16":
                case "Int32":
                case "Int64":
                case "Int128":
                case "Int256":
                case "UInt":
                case "UInt8":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                case "UInt128":
                case "UInt256":
                case "Word8":
                case "Word16":
                case "Word32":
                case "Word64":
                case "Fix64":
                case "UFix64":
                case "Bool":
                case "String":
                case "Address":
                case "Any":
                case "AnyStruct":
                case "AnyResource":
                case "Type":
                case "Void":
                case "Never":
                case "Character":
                case "Bytes":
                case "Number":
                case "SignedNumber":
                case "Integer":
                case "SignedInteger":
                case "FixedPoint":
                case "SignedFixedPoint":
                case "Path":
                case "CapabilityPath":
                case "StoragePath":
                case "PublicPath":
                case "PrivatePath":
                case "AuthAccount":
                case "PublicAccount":
                case "AuthAccount.Keys":
                case "PublicAccount.Keys":
                case "AuthAccount.Contracts":
                case "PublicAccount.Contracts":
                case "DeployedContract":
                case "AccountKey":
                case "Block":
                    return new SimpleTypeDefinition(kind);
                // --end simple types
                //TODO not supported below
                case "Function":
                case "Restriction":
                case "Optional":
                case "VariableSizedArray":
                case "ConstantSizedArray":
                case "Reference":
                default:
                    throw new NotImplementedException($"Unknown or unsupported type {kind}");
            }
        }
    }
}
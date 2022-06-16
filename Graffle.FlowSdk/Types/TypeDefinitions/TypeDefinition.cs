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
    public abstract class TypeDefinition : ITypeDefinition
    {
        [JsonPropertyName("kind")]
        public abstract string Kind { get; }

        public abstract Dictionary<string, dynamic> Flatten();

        public abstract string AsJsonCadenceDataFormat();

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
                    var typeId = root["typeID"];

                    var initializersJson = root["initializers"];
                    var parsedInitializers = JsonDocument.Parse(initializersJson);
                    var initializersArr = parsedInitializers.RootElement.EnumerateArray();

                    List<InitializerTypeDefinition> initializers = new List<InitializerTypeDefinition>();
                    foreach (var init in initializersArr)
                    {
                        var initJson = init.GetRawText();
                        var initType = InitializerTypeDefinition.FromJson(initJson);
                        initializers.Add(initType);
                    }

                    var fieldsJson = root["fields"];
                    var parsedFields = JsonDocument.Parse(fieldsJson);
                    var fieldsArr = parsedFields.RootElement.EnumerateArray();

                    List<FieldTypeDefinition> fields = new List<FieldTypeDefinition>();
                    foreach (var field in fieldsArr)
                    {
                        var fieldJson = field.GetRawText();
                        var fieldType = FieldTypeDefinition.FromJson(fieldJson);
                        fields.Add(fieldType);
                    }

                    return new CompositeTypeDefinition(kind, typeId, initializers, fields);
                case "Capability":
                    var innerTypeJson = root.FirstOrDefault(x => x.Key == "type").Value.ToString();
                    var innerType = innerTypeJson == string.Empty ? null : TypeDefinition.FromJson(innerTypeJson);
                    return new CapabilityTypeDefinition(kind, innerType);
                case "Dictionary":
                    var key = TypeDefinition.FromJson(root["key"]);
                    var value = TypeDefinition.FromJson(root["value"]);

                    return new DictionaryTypeDefinition(key, value);
                case "Reference":
                    var authorized = Convert.ToBoolean(root["authorized"]);
                    var type = TypeDefinition.FromJson(root["type"]);

                    return new ReferenceTypeDefinition(authorized, type);
                case "Optional":
                    var optionalType = TypeDefinition.FromJson(root["type"]);
                    return new OptionalTypeDefinition(optionalType);
                case "Restriction":
                    var topLevelType = TypeDefinition.FromJson(root["type"]);
                    var typeIda = root["typeID"];

                    var restrictionsJson = root["restrictions"];

                    var parsedRestrictions = JsonDocument.Parse(restrictionsJson);
                    var restrictionsArr = parsedRestrictions.RootElement.EnumerateArray();
                    List<TypeDefinition> restrictionList = new List<TypeDefinition>();
                    foreach (var r in restrictionsArr)
                    {
                        if (r.ValueKind == JsonValueKind.Object)
                        {
                            var tmpJson = r.GetRawText();
                            var tmpRestriction = TypeDefinition.FromJson(tmpJson);
                            restrictionList.Add(tmpRestriction);
                        }
                        else //todo fix this: workaround for non-json object in restriction array
                        {
                            var tmp = new SimpleTypeDefinition(r.GetRawText());
                            restrictionList.Add(tmp);
                        }
                    }

                    return new RestrictedTypeDefinition(typeIda, topLevelType, restrictionList);
                //simple types https://docs.onflow.org/cadence/json-cadence-spec/#simple-types
                case "VariableSizedArray":
                    var variableArrayType = TypeDefinition.FromJson(root["type"]);

                    return new VariableSizedArrayDefinition(variableArrayType);
                case "ConstantSizedArray":
                    var constantArrayType = TypeDefinition.FromJson(root["type"]);
                    var size = Convert.ToUInt64(root["size"]);

                    return new ConstantSizedArrayTypeDefinition(constantArrayType, size);
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
                default:
                    throw new NotImplementedException($"Unknown or unsupported type {kind}");
            }
        }
    }
}
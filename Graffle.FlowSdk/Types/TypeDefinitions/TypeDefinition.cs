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

        public abstract dynamic Flatten();

        public abstract string AsJsonCadenceDataFormat();

        public static ITypeDefinition FromJson(string json)
        {
            /*  TODO:
                All of this logic should really be contained within each TypeDefinition implementation
                and ITypeDefinition should expose a FromJson(string) function
            */
            var parsedJson = JsonDocument.Parse(json);

            if (parsedJson.RootElement.ValueKind == JsonValueKind.String)
            {
                /*
                    https://docs.onflow.org/cadence/json-cadence-spec/#repeated-types
                    When a composite type appears more than once in cadence json the type is represented just by its name to save space
                    eg "restrictions" : [ "A.f233dcee88fe0abe.FungibleToken.Receiver" ] - here we just have a string instead of a json object
                */
                var text = parsedJson.RootElement.GetString();
                return new RepeatedTypeDefinition(text);
            }

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
                    var compositeInitializers = GetInitializersFromJson(initializersJson);

                    var fieldsJson = root["fields"];
                    var compositeFields = GetFieldsFromJson(fieldsJson);

                    return new CompositeTypeDefinition(kind, typeId, compositeInitializers, compositeFields);
                case "Capability":
                    var capabilityTypeJson = root["type"];
                    var capabilityType = capabilityTypeJson == string.Empty ? null : TypeDefinition.FromJson(capabilityTypeJson);
                    return new CapabilityTypeDefinition(kind, capabilityType);
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
                    var restrictionType = TypeDefinition.FromJson(root["type"]);
                    var restrictionTypeId = root["typeID"];

                    //todo this can be a function
                    var restrictionsJson = root["restrictions"];
                    var parsedRestrictions = JsonDocument.Parse(restrictionsJson);
                    var restrictionsArr = parsedRestrictions.RootElement.EnumerateArray();
                    List<ITypeDefinition> restrictionList = new List<ITypeDefinition>();
                    foreach (var r in restrictionsArr)
                    {
                        var tmpJson = r.GetRawText();
                        var tmpRestriction = TypeDefinition.FromJson(tmpJson);
                        restrictionList.Add(tmpRestriction);
                    }

                    return new RestrictedTypeDefinition(restrictionTypeId, restrictionType, restrictionList);
                case "VariableSizedArray":
                    var variableArrayType = TypeDefinition.FromJson(root["type"]);

                    return new VariableSizedArrayTypeDefinition(variableArrayType);
                case "ConstantSizedArray":
                    var constantArrayType = TypeDefinition.FromJson(root["type"]);
                    var size = Convert.ToUInt64(root["size"]);

                    return new ConstantSizedArrayTypeDefinition(constantArrayType, size);
                case "Enum":
                    var enumType = TypeDefinition.FromJson(root["type"]);
                    var enumTypeId = root["typeID"];

                    var enumFieldJson = root["fields"];
                    var enumFields = GetFieldsFromJson(enumFieldJson);

                    var enumInitializerJson = root["initializers"];
                    var enumInitializers = GetInitializersFromJson(enumInitializerJson);

                    return new EnumTypeDefinition(enumType, enumTypeId, enumFields, enumInitializers);
                case "Function":
                    var functionTypeId = root["typeID"];
                    var returnType = TypeDefinition.FromJson(root["return"]);
                    var functionParameters = GetParametersFromJson(root["parameters"]);

                    return new FunctionTypeDefinition(functionTypeId, functionParameters, returnType);
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
                default:
                    throw new NotImplementedException($"Unknown or unsupported type {kind}, json {json}");
            }
        }

        //TODO these two functions can be simplified into a single generic function
        //do this when adding FromJson to ITypeDefinition
        private static List<FieldTypeDefinition> GetFieldsFromJson(string json)
        {
            List<FieldTypeDefinition> res = new List<FieldTypeDefinition>();

            var parsedJson = JsonDocument.Parse(json);
            var jsonArr = parsedJson.RootElement.EnumerateArray();
            foreach (var item in jsonArr)
            {
                var tmpJson = item.GetRawText();
                var field = FieldTypeDefinition.FromJson(tmpJson);
                res.Add(field);
            }

            return res;
        }

        private static List<InitializerTypeDefinition> GetInitializersFromJson(string json)
        {
            List<InitializerTypeDefinition> res = new List<InitializerTypeDefinition>();

            var parsedJson = JsonDocument.Parse(json);
            var jsonArr = parsedJson.RootElement.EnumerateArray();
            foreach (var item in jsonArr)
            {
                var tmpJson = item.GetRawText();
                var initializer = InitializerTypeDefinition.FromJson(tmpJson);
                res.Add(initializer);
            }

            return res;
        }

        private static List<ParameterTypeDefinition> GetParametersFromJson(string json)
        {
            List<ParameterTypeDefinition> res = new List<ParameterTypeDefinition>();

            var parsedJson = JsonDocument.Parse(json);
            var jsonArr = parsedJson.RootElement.EnumerateArray();
            foreach (var item in jsonArr)
            {
                var tmpJson = item.GetRawText();
                var parameter = ParameterTypeDefinition.FromJson(tmpJson);
                res.Add(parameter);
            }

            return res;
        }
    }
}
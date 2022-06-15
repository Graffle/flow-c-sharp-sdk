using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class CompositeTypeDefinition : TypeDefinition
    {
        public CompositeTypeDefinition(string kind, string typeId, List<InitializerTypeDefinition> initializers, List<FieldTypeDefinition> fields)
        {
            Kind = kind;
            TypeId = typeId;
            Fields = fields;
            Type = string.Empty; //https://docs.onflow.org/cadence/json-cadence-spec/#composite-types
            Initializers = initializers;
        }

        [JsonPropertyName("kind")]
        public override string Kind { get; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("typeID")]
        public string TypeId { get; set; }

        [JsonPropertyName("initializers")]
        public List<InitializerTypeDefinition> Initializers { get; set; }

        [JsonPropertyName("fields")]
        public List<FieldTypeDefinition> Fields { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var fields = FieldsAsJson();
            var fieldsArrayString = $"[{string.Join(",", fields)}]";

            var initializers = InitializersAsJson();
            var initializersArrayString = $"[{string.Join(",", initializers)}]";

            return $"{{\"kind\":\"{Kind}\",\"typeID\":\"{TypeId}\",\"fields\":{fieldsArrayString},\"initializers\":{initializersArrayString},\"type\":\"{Type}\"}}";
        }

        public IEnumerable<string> FieldsAsJson()
        {
            foreach (var field in Fields)
            {
                yield return field.AsJsonCadenceDataFormat();
            }
        }

        public IEnumerable<string> InitializersAsJson()
        {
            foreach (var init in Initializers)
            {
                yield return init.AsJsonCadenceDataFormat();
            }
        }

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("type", Type);
            res.Add("typeID", TypeId);

            var initializers = new List<Dictionary<string, dynamic>>();
            foreach (var init in Initializers)
            {
                initializers.Add(init.Flatten());
            }
            res.Add("initializers", initializers);

            var fields = new List<Dictionary<string, dynamic>>();
            foreach (var field in Fields)
            {
                fields.Add(field.Flatten());
            }

            res.Add("fields", fields);

            return res;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.StructuredTypes
{
    public class CompositeTypeDefinition : TypeDefinition
    {
        public CompositeTypeDefinition(string kind, Dictionary<string, string> data)
        {
            Kind = kind;
            Type = data["type"];
            TypeId = data["typeID"];

            var fieldsJson = data["fields"];
            var parsedFieldsJson = JsonDocument.Parse(fieldsJson);
            var allFields = parsedFieldsJson.RootElement.EnumerateArray().Select(x => x.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString()));
            foreach (var field in allFields)
            {
                var id = field["id"];
                var type = TypeDefinition.FromJson(field["type"]);

                Fields.Add(new CompositeTypeDefinitionField(id, type));
            }

            var initializersJson = data["initializers"];
            var parsedInitializersJson = JsonDocument.Parse(initializersJson);
            var allInitializers = parsedInitializersJson.RootElement.EnumerateArray().Select(x => x.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString()));
            foreach (var init in allInitializers)
            {
                var id = init["id"];
                var label = init["label"];
                var type = TypeDefinition.FromJson(init["type"]);

                Initializers.Add(new CompositeTypeDefinitionInitializer(id, label, type));
            }
        }

        [JsonPropertyName("kind")]
        public override string Kind { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("typeID")]
        public string TypeId { get; set; }

        [JsonPropertyName("initializers")]
        public List<CompositeTypeDefinitionInitializer> Initializers { get; set; } = new List<CompositeTypeDefinitionInitializer>();

        [JsonPropertyName("fields")]
        public List<CompositeTypeDefinitionField> Fields { get; set; } = new List<CompositeTypeDefinitionField>();

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
                var id = field.Id;
                var typeJson = field.Type.AsJsonCadenceDataFormat();

                yield return $"{{\"id\":\"{id}\",\"type\":{typeJson}}}";
            }
        }

        public IEnumerable<string> InitializersAsJson()
        {
            foreach (var init in Initializers)
            {
                var id = init.Id;
                var label = init.Label;
                var typeJson = init.Type.AsJsonCadenceDataFormat();

                yield return $"{{\"id\":\"{id}\",\"label\":\"{label}\",\"type\":{typeJson}}}";
            }
        }
    }
}
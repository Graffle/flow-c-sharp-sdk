using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class CompositeTypeDefinition : TypeDefinition
    {
        public CompositeTypeDefinition(string kind, string type, string typeId, IEnumerable<CompositeTypeDefinitionInitializer> initializers, IEnumerable<CompositeTypeDefinitionField> fields)
        {
            Kind = kind;
            Type = type;
            TypeId = typeId;
            Fields = fields.ToList();
            Initializers = initializers.ToList();
        }

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
        public override string Kind { get; }

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

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("type", Type);
            res.Add("typeID", TypeId);

            var initializers = new List<Dictionary<string, dynamic>>();
            foreach (var init in Initializers)
            {
                var initializerDict = new Dictionary<string, dynamic>();

                initializerDict.Add("label", init.Label);
                initializerDict.Add("id", init.Id);
                initializerDict.Add("type", init.Type.Flatten());

                initializers.Add(initializerDict);
            }
            res.Add("initializers", initializers);

            var fields = new List<Dictionary<string, dynamic>>();
            foreach (var field in Fields)
            {
                var fieldDict = new Dictionary<string, dynamic>();
                fieldDict.Add("id", field.Id);
                fieldDict.Add("type", field.Type.Flatten());

                fields.Add(fieldDict);
            }

            res.Add("fields", fields);

            return res;
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
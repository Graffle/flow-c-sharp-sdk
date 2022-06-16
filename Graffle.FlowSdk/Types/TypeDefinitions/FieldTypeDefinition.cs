using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class FieldTypeDefinition : TypeDefinitionBase
    {
        public FieldTypeDefinition(string id, TypeDefinition type)
        {
            Id = id;
            Type = type;
        }

        public string Id { get; set; }
        public TypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"id\":\"{Id}\",\"type\":{Type.AsJsonCadenceDataFormat()}}}";
        }

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("id", Id);
            res.Add("type", Type.Flatten());

            return res;
        }

        public static FieldTypeDefinition FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString());

            var id = root["id"];

            var typeJson = root["type"];
            var type = TypeDefinition.FromJson(typeJson);

            return new FieldTypeDefinition(id, type);
        }
    }
}
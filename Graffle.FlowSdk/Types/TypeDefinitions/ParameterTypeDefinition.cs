using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class ParameterTypeDefinition : ITypeDefinition
    {
        public ParameterTypeDefinition(string label, string id, TypeDefinition type)
        {
            Label = label;
            Id = id;
            Type = type;
        }

        public string Label { get; set; }
        public string Id { get; set; }
        public TypeDefinition Type { get; set; }

        public string AsJsonCadenceDataFormat()
        {
            return $"{{\"label\":\"{Label}\",\"id\":\"{Id}\",\"type\":{Type.AsJsonCadenceDataFormat()}}}";
        }

        public dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("label", Label);
            res.Add("id", Id);
            res.Add("type", Type.Flatten());

            return res;
        }

        public static ParameterTypeDefinition FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString());

            var label = root["label"];
            var id = root["id"];

            var typeJson = root["type"];
            var type = TypeDefinition.FromJson(typeJson);

            return new ParameterTypeDefinition(label, id, type);
        }
    }
}
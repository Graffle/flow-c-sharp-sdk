using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class CompositeTypeDefinitionInitializer
    {
        public CompositeTypeDefinitionInitializer(string id, string label, TypeDefinition type)
        {
            Label = label;
            Id = id;
            Type = type;
        }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }
    }
}
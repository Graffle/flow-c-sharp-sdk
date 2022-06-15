using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.StructuredTypes
{
    public class CompositeTypeDefinitionField
    {
        public CompositeTypeDefinitionField(string id, TypeDefinition type)
        {
            Id = id;
            Type = type;
        }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }
    }
}
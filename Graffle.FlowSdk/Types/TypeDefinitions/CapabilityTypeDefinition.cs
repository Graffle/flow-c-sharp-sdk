using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.StructuredTypes
{
    public class CapabilityTypeDefinition : TypeDefinition
    {
        public CapabilityTypeDefinition(string kind, TypeDefinition type)
        {
            Kind = kind;
            Type = type;
        }

        [JsonPropertyName("kind")]
        public override string Kind { get; set; }

        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var typeJson = Type?.AsJsonCadenceDataFormat() ?? $"\"\""; //just double quotes if no type

            return $"{{\"kind\":\"{Kind}\",\"type\":{typeJson}}}";
        }
    }
}
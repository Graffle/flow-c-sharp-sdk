using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class CapabilityTypeDefinition : TypeDefinition
    {
        public CapabilityTypeDefinition(string kind, TypeDefinition type)
        {
            Kind = kind;
            Type = type;
        }

        [JsonPropertyName("kind")]
        public override string Kind { get; }

        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var typeJson = Type?.AsJsonCadenceDataFormat() ?? $"\"\""; //just double quotes if no type

            return $"{{\"kind\":\"{Kind}\",\"type\":{typeJson}}}";
        }

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            if (Type != null)
                res.Add("type", Type.Flatten());
            else
                res.Add("type", string.Empty);

            return res;
        }
    }
}
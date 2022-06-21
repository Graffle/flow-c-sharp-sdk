using Graffle.FlowSdk.Types.TypeDefinitions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class FlowType : FlowValueType
    {
        public FlowType(ITypeDefinition staticType)
        {
            Data = staticType;
        }

        public static FlowType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            if (!parsedJson.RootElement.TryGetProperty("value", out var jsonElement))
            {
                //this function might be called with just the inner json for the value node
                //just read directly from root
                jsonElement = parsedJson.RootElement;
            }

            var staticTypeJson = jsonElement.GetProperty("staticType").GetRawText();
            var data = TypeDefinition.FromJson(staticTypeJson);

            return new FlowType(data);
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.FLOW_TYPE_NAME;

        [JsonPropertyName("data")]
        public ITypeDefinition Data { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var dataJson = Data.AsJsonCadenceDataFormat();

            return $"{{\"type\":\"{Type}\",\"value\":{{\"staticType\":{dataJson}}}}}";
        }

        public override string DataAsJson()
        {
            return Data.AsJsonCadenceDataFormat();
        }
    }
}

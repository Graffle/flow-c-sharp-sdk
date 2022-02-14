using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class FlowType : FlowValueType<string>
    {
        public FlowType(string value) : base(value)
        {
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

            var staticType = jsonElement.GetProperty("staticType").ToString();
            var result = new FlowType(staticType);
            return result;
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.FLOW_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":{{\"staticType\":\"{Data}\"}}}}";
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class Fix64Type : FlowValueType<decimal>
    {
        public static Fix64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = decimal.Parse(value.GetString());
            return new Fix64Type(attempt);
        }
        public Fix64Type(decimal value) : base(value)
        {
        }

        public Fix64Type(string value) : base(decimal.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.FIX64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
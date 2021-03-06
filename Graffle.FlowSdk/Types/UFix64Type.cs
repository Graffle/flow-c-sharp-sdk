using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UFix64Type : FlowValueType<decimal>
    {
        public static UFix64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = decimal.Parse(value.GetString());
            return new UFix64Type(attempt);
        }
        public UFix64Type(decimal value) : base(value)
        {
        }

        public UFix64Type(string value) : base(decimal.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.UFIX64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
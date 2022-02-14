using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UInt64Type : FlowValueType<UInt64>
    {
        public static UInt64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt64.Parse(value.GetString());
            return new UInt64Type(attempt);
        }
        public UInt64Type(UInt64 value) : base(value)
        {
        }

        public UInt64Type(string value) : base(UInt64.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.UINT64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
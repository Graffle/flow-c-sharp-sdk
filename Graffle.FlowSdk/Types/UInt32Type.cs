using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UInt32Type : FlowValueType<UInt32>
    {
        public static UInt32Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt32.Parse(value.GetString());
            return new UInt32Type(attempt);
        }
        public UInt32Type(uint value) : base(value)
        {
        }
        public UInt32Type(string value) : base(UInt32.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.UINT32_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
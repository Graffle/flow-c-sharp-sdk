using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UInt16Type : FlowValueType<uint>
    {
        public static UInt16Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt32.Parse(value.GetString());
            return new UInt16Type(attempt);
        }
        public UInt16Type(uint value) : base(value)
        {
        }
        public UInt16Type(string value) : base(UInt16.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.UINT16_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UIntType : FlowValueType<uint>
    {
        public static UIntType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt32.Parse(value.GetString());
            return new UIntType(attempt);
        }
        public UIntType(uint value) : base(value)
        {
        }
        public UIntType(string value) : base(UInt32.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => UINT_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
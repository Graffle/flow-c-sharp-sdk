using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class Int16Type : FlowValueType<Int16>
    {
        public static Int16Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = Int16.Parse(value.GetString());
            return new Int16Type(attempt);
        }
        public Int16Type(Int16 value) : base(value)
        {
        }
        public Int16Type(string value) : base(Int16.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => INT16_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
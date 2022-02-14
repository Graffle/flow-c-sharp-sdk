using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class Int8Type : FlowValueType<int>
    {
        public static Int8Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = Int32.Parse(value.GetString());
            return new Int8Type(attempt);
        }
        public Int8Type(int value) : base(value)
        {
        }
        public Int8Type(string value) : base(Int16.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.INT8_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
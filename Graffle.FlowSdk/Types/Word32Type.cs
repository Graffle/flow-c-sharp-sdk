using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class Word32Type : FlowValueType<uint>
    {
        public Word32Type(uint value) : base(value)
        {
        }

        public static Word32Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = uint.Parse(value.GetString());
            return new Word32Type(attempt);
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.WORD32_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
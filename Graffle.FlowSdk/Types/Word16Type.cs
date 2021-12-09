using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Word16Type : FlowValueType<uint>
    {
        public Word16Type(uint value) : base(value)
        {
        }

        public static Word16Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = uint.Parse(value.GetString());
            return new Word16Type(attempt);
        }

        public override string Type
            => WORD16_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
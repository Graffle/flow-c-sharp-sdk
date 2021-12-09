using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{

    public class Word64Type : FlowValueType<UInt64>
    {
        public Word64Type(UInt64 value) : base(value)
        {
        }

        public static Word64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt64.Parse(value.GetString());
            return new Word64Type(attempt);
        }

        public override string Type
            => WORD64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
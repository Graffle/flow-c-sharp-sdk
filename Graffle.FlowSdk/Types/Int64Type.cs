using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Int64Type : FlowValueType<Int64>
    {
        public static Int64Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = Int64.Parse(value.GetString());
            return new Int64Type(attempt); ;
        }
        public Int64Type(Int64 value) : base(value)
        {
        }
        public Int64Type(string value) : base(Int16.Parse(value))
        {
        }

        public override string Type
            => INT64_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
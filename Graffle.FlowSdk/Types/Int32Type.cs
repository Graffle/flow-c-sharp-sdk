using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Int32Type : FlowValueType<Int32>
    {
        public static Int32Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = Int16.Parse(value.GetString());
            return new Int32Type(attempt); ;
        }
        public Int32Type(Int32 value) : base(value)
        {
        }
        public Int32Type(string value) : base(Int32.Parse(value))
        {
        }

        public override string Type
            => INT32_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
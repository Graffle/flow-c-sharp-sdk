using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class IntType : FlowValueType<int>
    {
        public static IntType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = Int32.Parse(value.GetString());
            return new IntType(attempt);
        }
        public IntType(int value) : base(value)
        {
        }
        public IntType(string value) : base(Int32.Parse(value))
        {
        }

        public override string Type
            => INT_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
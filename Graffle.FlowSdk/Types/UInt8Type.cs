using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class UInt8Type : FlowValueType<uint>
    {
        public static UInt8Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = UInt32.Parse(value.GetString());
            return new UInt8Type(attempt); ;
        }
        public UInt8Type(uint value) : base(value)
        {
        }
        public UInt8Type(string value) : base(UInt16.Parse(value))
        {
        }

        public override string Type
            => UINT8_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
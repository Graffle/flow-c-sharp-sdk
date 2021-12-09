using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Int128Type : FlowValueType<System.Numerics.BigInteger>
    {
        public static Int128Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = System.Numerics.BigInteger.Parse(value.GetString());
            return new Int128Type(attempt); ;
        }
        public Int128Type(System.Numerics.BigInteger value) : base(value)
        {
        }
        public Int128Type(string value) : base(System.Numerics.BigInteger.Parse(value))
        {
        }

        public override string Type
            => INT128_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
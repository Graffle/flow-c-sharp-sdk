using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class Int256Type : FlowValueType<System.Numerics.BigInteger>
    {
        public static Int256Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = System.Numerics.BigInteger.Parse(value.GetString());
            return new Int256Type(attempt);
        }
        public Int256Type(System.Numerics.BigInteger value) : base(value)
        {
        }
        public Int256Type(string value) : base(System.Numerics.BigInteger.Parse(value))
        {
        }

        public override string Type
            => INT256_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
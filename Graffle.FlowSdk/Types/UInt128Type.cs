using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class UInt128Type : FlowValueType<System.Numerics.BigInteger>
    {
        public static UInt128Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = System.Numerics.BigInteger.Parse(value.GetString());
            return new UInt128Type(attempt);
        }
        public UInt128Type(System.Numerics.BigInteger value) : base(value)
        {
        }
        public UInt128Type(string value) : base(System.Numerics.BigInteger.Parse(value))
        {
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.UINT128_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
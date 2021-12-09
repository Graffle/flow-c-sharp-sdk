using System;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class UInt256Type : FlowValueType<System.Numerics.BigInteger>
    {
        public static UInt256Type FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var attempt = System.Numerics.BigInteger.Parse(value.GetString());
            return new UInt256Type(attempt);
        }
        public UInt256Type(System.Numerics.BigInteger value) : base(value)
        {
        }
        public UInt256Type(string value) : base(System.Numerics.BigInteger.Parse(value))
        {
        }

        public override string Type
            => UINT256_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
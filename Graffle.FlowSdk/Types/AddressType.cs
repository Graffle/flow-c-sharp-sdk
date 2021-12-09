using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class AddressType : FlowValueType<string>
    {
        public AddressType(string value) : base(value)
        {
        }

        public override string Type
            => ADDRESS_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";

        public static AddressType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var attempt = parsedJson.RootElement.GetProperty("value").ToString();
            return new AddressType(attempt);
        }
    }
}
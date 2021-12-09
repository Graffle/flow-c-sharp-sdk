using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class StringType : FlowValueType<string>
    {
        public StringType(string value) : base(value)
        {
        }

        public static StringType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            return new StringType(value.GetString());
        }

        public override string Type
            => STRING_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
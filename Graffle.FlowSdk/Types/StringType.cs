using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Graffle.FlowSdk.Types
{
    public class StringType : FlowValueType<string>
    {
        private static Regex NEWLINE_REGEX = new Regex("\n", RegexOptions.Compiled);
        private const string ESCAPED_NEWLINE = "\\n";

        public StringType(string value) : base(value)
        {
        }

        public static StringType FromJson(string json)
        {
            var escaped = NEWLINE_REGEX.Replace(json, ESCAPED_NEWLINE);

            var parsedJson = JsonDocument.Parse(escaped);
            var value = parsedJson.RootElement.GetProperty("value");
            return new StringType(value.GetString());
        }

        [JsonPropertyName("type")]
        public override string Type
                    => Constants.STRING_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":\"{Data}\"}}";
    }
}
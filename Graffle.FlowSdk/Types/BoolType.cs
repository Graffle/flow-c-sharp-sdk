using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class BoolType : FlowValueType<bool>
    {
        public BoolType(bool value) : base(value)
        {
        }

        public static BoolType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            return new BoolType(value.GetBoolean());
        }

        [JsonPropertyName("type")]
        public override string Type
                   => Constants.BOOL_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => System.Text.Json.JsonSerializer.Serialize(new { type = Type, value = Data });
    }
}
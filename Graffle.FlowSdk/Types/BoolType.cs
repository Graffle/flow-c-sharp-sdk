using System;
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
            return value.ValueKind switch
            {
                JsonValueKind.False => new BoolType(value.GetBoolean()),
                JsonValueKind.True => new BoolType(value.GetBoolean()),
                JsonValueKind.String => new BoolType(Convert.ToBoolean(value.GetString())),
                _ => throw new InvalidOperationException($"Invalid json element {value.ValueKind} encountered parsing bool type: {value.GetRawText()}")
            };
        }

        [JsonPropertyName("type")]
        public override string Type
                   => Constants.BOOL_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => System.Text.Json.JsonSerializer.Serialize(new { type = Type, value = Data });
    }
}
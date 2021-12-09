using System.Text.Json;

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

        public override string Type
            => BOOL_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => System.Text.Json.JsonSerializer.Serialize(new {type = Type, value = Data});
    }
}
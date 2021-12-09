using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class FlowType : FlowValueType<string>
    {
        public FlowType(string value) : base(value)
        {
        }

        public static FlowType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var value = parsedJson.RootElement.GetProperty("value");
            var staticType = value.GetProperty("staticType").ToString();
            var result = new FlowType(staticType);
            return result;
        }

        public override string Type
            => FLOW_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
            => $"{{\"type\":\"{Type}\",\"value\":{{\"staticType\":\"{Data}\"}}}}";
    }
}

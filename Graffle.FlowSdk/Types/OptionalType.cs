using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class OptionalType : FlowValueType
    {
        public OptionalType(FlowValueType value)
        {
            Data = value;
        }

        [JsonPropertyName("type")]
        public override string Type => Constants.OPTIONAL_TYPE_NAME;

        [JsonPropertyName("data")]
        public FlowValueType Data { get; set; }

        public static OptionalType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var attempt = parsedJson.RootElement.GetProperty("value");
            if (attempt.ValueKind == JsonValueKind.Null)
            {
                return new OptionalType(null);
            }
            else
            {
                var type = attempt.GetProperty("type").ToString() ?? null;
                var value = attempt.GetProperty("value").ToString();

                string jsonObject;
                if (FlowValueType.IsPrimitiveType(type))
                {
                    jsonObject = $"{{\"type\":\"{type}\",\"value\":\"{value}\"}}";
                }
                else
                {
                    //we're dealing with a complex type here and value is going to be a json string
                    //ie do not wrap value in quotes
                    jsonObject = $"{{\"type\":\"{type}\",\"value\":{value}}}";
                }

                var result = new OptionalType(FlowValueType.CreateFromCadence(type, jsonObject));
                return result;
            }
        }

        public override string AsJsonCadenceDataFormat()
        {
            var valueCadence = Data?.AsJsonCadenceDataFormat() ?? null;
            var result = string.Empty;
            if (valueCadence is null)
                result = $"{{\"type\":\"{Type}\",\"value\":null}}";
            else
                result = $"{{\"type\":\"{Type}\",\"value\":{valueCadence}}}";
            return result;
        }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
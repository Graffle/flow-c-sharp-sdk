using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class ArrayType : FlowValueType<List<FlowValueType>>
    {
        public ArrayType(List<FlowValueType> data) : base(data)
        {
        }
        [JsonPropertyName("type")]
        public override string Type => "Array";

        public override string AsJsonCadenceDataFormat()
        {
            var jsonElements = Data.Select(x => x.AsJsonCadenceDataFormat());
            var jsonArray = string.Join(',', jsonElements);

            return $"{{\"type\":\"{Type}\",\"value\":[{jsonArray}]}}";
        }

        public IEnumerable<object> Values()
        {
            var result = new List<object>();
            foreach (var item in Data)
            {
                var dynamicItem = (dynamic)item;
                result.Add(dynamicItem);
            }
            return result;
        }

        public static ArrayType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
            var fields = root.FirstOrDefault(z => z.Key == "value").Value.EnumerateArray().Select(h => h.EnumerateObject().ToDictionary(n => n.Name, n => n.Value.ToString()));
            var result = new ArrayType(new List<FlowValueType>());
            foreach (var field in fields)
            {
                var newItem = FlowValueType.Create(field["type"], field["value"]);
                result.Data.Add(newItem);
            }
            return result;
        }
    }
}
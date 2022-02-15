using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class StructType : FlowValueType
    {
        protected StructType() { }

        public StructType(string id, List<(string name, FlowValueType value)> fields)
        {
            Data = (id, fields);
        }

        public StructType((string id, List<(string name, FlowValueType value)> fields) value)
        {
            Data = value;
        }

        public string Id => Data.id;

        public List<(string name, FlowValueType value)> Fields => Data.fields;

        public (string id, List<(string name, FlowValueType value)> fields) Data { get; set; }

        [JsonPropertyName("type")]
        public override string Type => Constants.STRUCT_TYPE_NAME;

        public override string AsJsonCadenceDataFormat()
        {
            var valueArray = FieldsAsJson();
            var arrayString = $"[{string.Join(",", valueArray)}]";
            var result = $"{{\"type\":\"{Type}\",\"value\":{{\"fields\":{arrayString},\"id\":\"{Id}\"}}}}";
            return result;
        }

        public override string DataAsJson()
        {
            var valueArray = FieldsAsJson();
            var arrayString = $"[{string.Join(",", valueArray)}]";
            var result = $"{{\"fields\":{arrayString},\"id\":\"{Id}\"}}";
            return result;
        }

        public static StructType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
            var rootValue = root.FirstOrDefault(z => z.Key == "value").Value;

            var id = rootValue.GetProperty("id").GetString(); //struct id
            var fields = rootValue.GetProperty("fields").EnumerateArray().Select(h => h.EnumerateObject().ToDictionary(n => n.Name, n => n.Value.ToString())); //field array

            var parsedFields = new List<(string, FlowValueType)>();
            foreach (var item in fields)
            {
                //name
                var name = item.Values.First();

                //value
                var valueJson = JsonDocument.Parse(item.Values.Last());
                var valueJsonElementsKvp = valueJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
                var valueJsonType = valueJsonElementsKvp.FirstOrDefault(z => z.Key == "type").Value;
                var flowValue = FlowValueType.CreateFromCadence(valueJsonType.GetString(), item.Values.Last());

                parsedFields.Add((name, flowValue));
            }

            var result = new StructType(id, parsedFields);
            return result;
        }

        private IEnumerable<string> FieldsAsJson()
        {
            foreach (var item in Data.fields)
            {
                var name = item.name;
                var value = item.value.AsJsonCadenceDataFormat();
                var entry = $"{{\"name\":\"{name}\",\"value\":{value}}}";
                yield return entry;
            }
        }
    }
}
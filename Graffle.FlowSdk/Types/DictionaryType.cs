using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class DictionaryType : FlowValueType
    {
        public DictionaryType()
        {
        }

        public DictionaryType(Dictionary<FlowValueType, FlowValueType> value)
        {
            Data = value;
        }

        [JsonPropertyName("type")]
        public override string Type => "Dictionary";

        [JsonPropertyName("data")]
        public Dictionary<FlowValueType, FlowValueType> Data { get; set; } = new Dictionary<FlowValueType, FlowValueType>();

        public static DictionaryType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            JsonElement values;
            if (parsedJson.RootElement.ValueKind == JsonValueKind.Array)
            {
                //this function might be called with just the inner json for the value node
                //just read directly from root
                values = parsedJson.RootElement;
            }
            else
            {
                var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
                values = root.FirstOrDefault(z => z.Key == "value").Value;
            }

            var dictionaryValues = values.EnumerateArray().Select(h => h.EnumerateObject().ToDictionary(n => n.Name, n => n.Value.ToString()));
            var result = new DictionaryType();
            foreach (var item in dictionaryValues)
            {
                //Get the value portion out
                var valueJson = JsonDocument.Parse(item["value"]);
                var valueJsonElementsKvp = valueJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
                var valueJsonType = valueJsonElementsKvp.FirstOrDefault(z => z.Key == "type").Value;
                var myValue = FlowValueType.CreateFromCadence(valueJsonType.GetString(), item["value"]);

                //Get the key portion
                var keyJson = JsonDocument.Parse(item["key"]);
                var keyJsonElementsKvp = keyJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
                var keyJsonType = keyJsonElementsKvp.FirstOrDefault(z => z.Key == "type").Value;
                var key = FlowValueType.CreateFromCadence(keyJsonType.GetString(), item["key"]);

                result.Data.Add(key, myValue);
            }
            return result;
        }

        public bool TryGetValueType<X>(FlowValueType<X> key, out FlowValueType result)
        {
            var keys = Data.Where(x => x.Key.Type == key.Type);
            if (keys != null && keys.Any())
            {
                foreach (var item in keys)
                {
                    var typeCastKey = (FlowValueType<X>)item.Key;
                    if (typeCastKey.Data.ToString() == key.Data.ToString())
                    {
                        result = item.Value;
                        return true;
                    }
                }
            }
            result = null;
            return false;
        }

        public override string AsJsonCadenceDataFormat()
        {
            var valueArray = new List<string>();
            foreach (var item in Data)
            {
                var keyString = item.Key.AsJsonCadenceDataFormat();
                var valueString = item.Value.AsJsonCadenceDataFormat();
                var entry = $"{{\"key\":{keyString},\"value\":{valueString}}}";
                valueArray.Add(entry);
            }

            var arrayString = $"[{string.Join(",", valueArray)}]";
            var result = $"{{\"type\":\"{Type}\",\"value\":{arrayString}}}";
            return result;
        }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
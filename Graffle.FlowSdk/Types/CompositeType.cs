using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    /// <summary>
    /// Struct, Resource, Event, Contract, Enum
    /// </summary>
    public class CompositeType : FlowValueType
    {
        private readonly string _type;

        public CompositeType(string type, string id, List<CompositeField> fields)
        {
            _type = type;
            Data = new CompositeData(id, fields);
        }

        public CompositeType(string type, CompositeData data)
        {
            _type = type;
            Data = data;
        }

        public string Id => Data.Id;

        public List<CompositeField> Fields => Data.Fields;

        public CompositeData Data { get; set; }

        public override string Type => _type;

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

        public static CompositeType FromJson(string type, string valueJson)
        {
            //compile it back to valid cadence json for the composite type
            //this can get called from optional type parsing where we're working with the
            //type and value json separately
            var json = $"{{\"type\":\"{type}\",\"value\":{valueJson}}}";

            return CompositeType.FromJson(json);
        }

        public static CompositeType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
            var type = root.FirstOrDefault(z => z.Key == "type").Value.GetString();
            var rootValue = root.FirstOrDefault(z => z.Key == "value").Value;

            var id = rootValue.GetProperty("id").GetString(); //struct id
            var fields = rootValue.GetProperty("fields").EnumerateArray().Select(h => h.EnumerateObject().ToDictionary(n => n.Name, n => n.Value.ToString())); //field array

            var parsedFields = new List<CompositeField>();
            foreach (var item in fields)
            {
                //name
                var name = item.Values.First();

                //value
                var valueJson = JsonDocument.Parse(item.Values.Last());
                var valueJsonElementsKvp = valueJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
                var valueJsonType = valueJsonElementsKvp.FirstOrDefault(z => z.Key == "type").Value;
                var flowValue = FlowValueType.CreateFromCadence(valueJsonType.GetString(), item.Values.Last());

                parsedFields.Add(new CompositeField(name, flowValue));
            }

            var result = new CompositeType(type, id, parsedFields);
            return result;
        }

        private IEnumerable<string> FieldsAsJson()
        {
            foreach (var item in Data.Fields)
            {
                var name = item.Name;
                var value = item.Value.AsJsonCadenceDataFormat();
                var entry = $"{{\"name\":\"{name}\",\"value\":{value}}}";
                yield return entry;
            }
        }
    }
}
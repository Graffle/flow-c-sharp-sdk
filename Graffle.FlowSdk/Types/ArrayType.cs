using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{

    public static class Extensions
    {
        public static dynamic ToValueData(this ArrayType x)
        {
            var result = new List<dynamic>();
            foreach (var item in x.Data)
            {
                result.Add(((dynamic)item).Data);
            }

            return result;
        }
    }

    public class ArrayType : FlowValueType<List<FlowValueType>>
    {

        public ArrayType(List<FlowValueType> data) : base(data)
        {
        }

        public override string Type => "Array";

        public override string AsJsonCadenceDataFormat() {
            var jsonElements = Data.Select(x => x.AsJsonCadenceDataFormat());
            var jsonArray = string.Join(',', jsonElements);

            return $"{{\"type\":\"{Type}\",\"value\":\"[{jsonArray}\"]}}";
        }

        


        public static ArrayType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var root = parsedJson.RootElement.EnumerateObject().ToDictionary(x => x.Name, x => x.Value);
            var fields = root.FirstOrDefault(z => z.Key == "value").Value.EnumerateArray().Select(h => h.EnumerateObject().ToDictionary(n => n.Name, n => n.Value.ToString()));
            var result = new ArrayType(new List<FlowValueType>());
            foreach (var field in fields)
            {
                var newItem = FlowValueType.Create(field.First().Value, field.Last().Value);
                result.Data.Add(newItem);
            }
            return result;
        }
    }
}
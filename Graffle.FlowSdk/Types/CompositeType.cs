using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class CompositeType : FlowValueType
    {
        public CompositeType()
        {
        }
        public CompositeType(string type)
        {
            Type = type;
        }

        [JsonPropertyName("type")]
        public override string Type { get; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var fields = new List<string>();
            foreach (var field in Data)
            {
                var json = $"{{\"name\":\"{field.Key}\",\"value\":{field.Value}}}";
                fields.Add(json);
            }
            var result = $"{{\"type\":\"{Type}\",\"value\":{{\"id\":\"{Id}\",\"fields\":[{string.Join(',', fields)}]}}}}";
            return result;
        }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
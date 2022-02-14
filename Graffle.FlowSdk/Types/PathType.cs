using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class PathType : FlowValueType
    {
        private const string DOMAIN_NAME = "domain";
        private const string IDENTIFIER_NAME = "identifier";

        public PathType(Dictionary<string, string> values)
         : this(values[DOMAIN_NAME], values[IDENTIFIER_NAME])
        { }

        public PathType(string domain, string identifier)
        {
            Data = new Dictionary<string, string>();
            Data.Add(DOMAIN_NAME, domain);
            Data.Add(IDENTIFIER_NAME, identifier);
        }

        [JsonPropertyName("domain")]
        public string Domain => Data[DOMAIN_NAME];

        [JsonPropertyName("identifier")]
        public string Identifier => Data[IDENTIFIER_NAME];

        [JsonPropertyName("type")]
        public override string Type => Constants.PATH_TYPE_NAME;

        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        public static PathType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            if (!parsedJson.RootElement.TryGetProperty("value", out var jsonElement))
            {
                //this function might be called with just the inner json for the value node
                //just read directly from root
                jsonElement = parsedJson.RootElement;
            }

            var domain = jsonElement.GetProperty(DOMAIN_NAME).ToString();
            var identifier = jsonElement.GetProperty(IDENTIFIER_NAME).ToString();

            var result = new PathType(domain, identifier);
            return result;
        }

        public override string AsJsonCadenceDataFormat()
        {
            var result = $"{{\"type\":\"{Type}\",\"value\":{{\"domain\":\"{Domain}\",\"identifier\":\"{Identifier}\"}}}}";
            return result;
        }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
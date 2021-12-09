using System.Collections.Generic;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class PathType : FlowValueType
    {

        private const string DOMAIN_NAME = "domain";
        private const string IDENTIFIER_NAME = "identifier";

        public PathType(string domain, string identifier)
        {
            Data = new Dictionary<string, string>();
            Data.Add(DOMAIN_NAME, domain);
            Data.Add(IDENTIFIER_NAME, identifier);
        }

        public string Domain => Data[DOMAIN_NAME];
        public string Identifier => Data[IDENTIFIER_NAME];

        public override string Type => PATH_TYPE_NAME;

        public Dictionary<string, string> Data { get; set; }

        public static PathType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var attempt = parsedJson.RootElement.GetProperty("value");
            var domain = attempt.GetProperty(DOMAIN_NAME).ToString();
            var identifier = attempt.GetProperty(IDENTIFIER_NAME).ToString();

            var result = new PathType(domain,identifier);
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
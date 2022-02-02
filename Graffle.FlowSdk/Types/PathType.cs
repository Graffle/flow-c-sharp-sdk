using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public class CapabilityType : FlowValueType
    {
        private const string PATH_NAME = "path";
        private const string ADDRESS_NAME = "address";
        private const string BORROW_NAME = "borrowType";

        public CapabilityType(string path, string address, string borrowType)
        {
            Data = new Dictionary<string, string>();
            Data.Add(PATH_NAME, path);
            Data.Add(ADDRESS_NAME, address);
            Data.Add(BORROW_NAME, borrowType);
        }

        [JsonPropertyName("path")]
        public string Path => Data[PATH_NAME];

        [JsonPropertyName("address")]
        public string Address => Data[ADDRESS_NAME];

        [JsonPropertyName("borrowType")]
        public string BorrowType => Data[BORROW_NAME];

        [JsonPropertyName("type")]
        public override string Type => CAPABILITY_TYPE_NAME;

        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        public static CapabilityType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);
            var attempt = parsedJson.RootElement.GetProperty("value");
            var path = attempt.GetProperty(PATH_NAME).ToString();
            var address = attempt.GetProperty(ADDRESS_NAME).ToString();
            var borrow = attempt.GetProperty(BORROW_NAME).ToString();

            var result = new CapabilityType(path, address, borrow);
            return result;
        }

        public override string AsJsonCadenceDataFormat()
        {
            var result = $"{{\"type\":\"{Type}\",\"value\":{{\"path\":\"{Path}\",\"address\":\"{Address}\",\"borrowType\":\"{BorrowType}\"}}}}";
            return result;
        }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
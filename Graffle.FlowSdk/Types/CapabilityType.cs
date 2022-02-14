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


        public CapabilityType(Dictionary<string, string> values)
            : this(values[PATH_NAME], values[ADDRESS_NAME], values[BORROW_NAME])
        { }

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
        public override string Type => Constants.CAPABILITY_TYPE_NAME;

        [JsonPropertyName("data")]
        public Dictionary<string, string> Data { get; set; }

        public static CapabilityType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            if (!parsedJson.RootElement.TryGetProperty("value", out var jsonElement))
            {
                //this function might be called with just the inner json for the value node
                //just read directly from root
                jsonElement = parsedJson.RootElement;
            }

            var path = jsonElement.GetProperty(PATH_NAME).ToString();
            var address = jsonElement.GetProperty(ADDRESS_NAME).ToString();
            var borrow = jsonElement.GetProperty(BORROW_NAME).ToString();

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
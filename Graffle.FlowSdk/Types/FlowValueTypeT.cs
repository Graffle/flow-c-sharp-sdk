using System.Text.Json.Serialization;

namespace Graffle.FlowSdk.Types
{
    public abstract class FlowValueType<T> : FlowValueType
    {
        protected FlowValueType(T data)
        {
            Data = data;
        }

        [JsonPropertyName("data")]
        public virtual T Data { get; }

        public override string DataAsJson()
            => Newtonsoft.Json.JsonConvert.SerializeObject(this.Data);
    }
}
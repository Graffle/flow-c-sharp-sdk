using Graffle.FlowSdk.Types.TypeDefinitions;
using System.Collections.Generic;
using System.Text.Json;

namespace Graffle.FlowSdk.Types
{
    public class FunctionType : FlowValueType
    {
        private readonly ITypeDefinition _functionType;
        public FunctionType(ITypeDefinition functionType)
        {
            _functionType = functionType;
        }

        public override string Type => "Function";

        public ITypeDefinition Data => _functionType;

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"type\":\"{Type}\",\"value\":{{\"functionType\":{_functionType.AsJsonCadenceDataFormat()}}}}}";
        }

        public override string DataAsJson()
        {
            return _functionType.AsJsonCadenceDataFormat();
        }

        public dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();
            res.Add("functionType", _functionType.Flatten());

            return res;
        }

        public static FunctionType FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            var value = parsedJson.RootElement.GetProperty("value").GetProperty("functionType");
            var functionType = TypeDefinition.FromJson(value.GetRawText());

            return new FunctionType(functionType);
        }
    }
}
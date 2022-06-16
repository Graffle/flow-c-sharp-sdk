using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class InitializerTypeDefinition : ITypeDefinition
    {
        public InitializerTypeDefinition(List<ParameterTypeDefinition> parameters)
        {
            Parameters = parameters;
        }

        public List<ParameterTypeDefinition> Parameters { get; set; }

        public string AsJsonCadenceDataFormat()
        {
            var parameters = ParametersAsJson();

            return $"[{string.Join(",", parameters)}]";
        }

        public dynamic Flatten()
        {
            List<dynamic> result = new List<dynamic>();

            foreach (var p in Parameters)
            {
                result.Add(p.Flatten());
            }

            return result;
        }

        public static InitializerTypeDefinition FromJson(string json)
        {
            var parsedJson = JsonDocument.Parse(json);

            var root = parsedJson.RootElement.EnumerateArray();
            List<ParameterTypeDefinition> parameters = new List<ParameterTypeDefinition>();
            foreach (var item in root)
            {
                var tmpJson = item.GetRawText();
                var parameter = ParameterTypeDefinition.FromJson(tmpJson);
                parameters.Add(parameter);
            }

            return new InitializerTypeDefinition(parameters);
        }

        private IEnumerable<string> ParametersAsJson()
        {
            foreach (var p in Parameters)
                yield return p.AsJsonCadenceDataFormat();
        }
    }
}
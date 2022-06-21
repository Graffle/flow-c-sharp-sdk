using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class FunctionTypeDefinition : TypeDefinition
    {
        public FunctionTypeDefinition(string typeId, List<ParameterTypeDefinition> parameters, ITypeDefinition returnType)
        {
            TypeId = typeId;
            Parameters = parameters;
            Return = returnType;
        }

        public override string Kind => "Function";

        public string TypeId { get; set; }

        public List<ParameterTypeDefinition> Parameters { get; set; }

        public ITypeDefinition Return { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var parameters = ParametersAsJson();
            var parametersArrayString = $"[{string.Join(",", parameters)}]";

            return $"{{\"kind\":\"{Kind}\",\"typeID\":\"{TypeId}\",\"parameters\":{parametersArrayString},\"return\":{Return.AsJsonCadenceDataFormat()}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("typeID", TypeId);

            var parameters = new List<dynamic>();
            foreach (var p in Parameters)
                parameters.Add(p.Flatten());

            res.Add("parameters", parameters);

            res.Add("return", Return.Flatten());

            return res;
        }

        private IEnumerable<string> ParametersAsJson()
        {
            foreach (var p in Parameters)
                yield return p.AsJsonCadenceDataFormat();
        }
    }
}
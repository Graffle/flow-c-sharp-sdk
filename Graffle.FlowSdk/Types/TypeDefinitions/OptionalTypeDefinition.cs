using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class OptionalTypeDefinition : TypeDefinition
    {
        public OptionalTypeDefinition(ITypeDefinition type)
        {
            Type = type;
        }

        public override string Kind => "Optional";

        public ITypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\",\"type\":{Type.AsJsonCadenceDataFormat()}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("type", Type.Flatten());

            return res;
        }
    }
}
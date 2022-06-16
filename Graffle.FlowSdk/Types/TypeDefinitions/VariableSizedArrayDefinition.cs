using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class VariableSizedArrayDefinition : TypeDefinition
    {
        public VariableSizedArrayDefinition(TypeDefinition type)
        {
            Type = type;
        }

        public override string Kind => "VariableSizedArray";

        public TypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\",\"type\":{Type}}}";
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
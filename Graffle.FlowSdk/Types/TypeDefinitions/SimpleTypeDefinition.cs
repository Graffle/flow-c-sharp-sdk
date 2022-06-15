using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.StructuredTypes
{
    public class SimpleTypeDefinition : TypeDefinition
    {
        public SimpleTypeDefinition(string kind)
        {
            Kind = kind;
        }

        public override string Kind { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\"}}";
        }

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);

            return res;
        }
    }
}
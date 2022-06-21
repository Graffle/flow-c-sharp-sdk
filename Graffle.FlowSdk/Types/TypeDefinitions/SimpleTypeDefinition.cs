using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class SimpleTypeDefinition : TypeDefinition
    {
        public SimpleTypeDefinition(string kind)
        {
            Kind = kind;
        }

        public override string Kind { get; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\"}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);

            return res;
        }
    }
}
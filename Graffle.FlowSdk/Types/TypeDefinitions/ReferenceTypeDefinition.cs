using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class ReferenceTypeDefinition : TypeDefinition
    {
        public ReferenceTypeDefinition(bool authorized, TypeDefinition type)
        {
            Authorized = authorized;
            Type = type;
        }

        public override string Kind => "Reference";

        public bool Authorized { get; set; }

        public TypeDefinition Type { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\",\"authorized\":{Authorized},\"type\":{Type.AsJsonCadenceDataFormat()}}}";
        }

        public override Dictionary<string, dynamic> Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("authorized", Authorized);
            res.Add("type", Type.Flatten());

            return res;
        }
    }
}
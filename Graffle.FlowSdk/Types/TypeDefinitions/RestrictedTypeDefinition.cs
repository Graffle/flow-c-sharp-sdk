using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class RestrictedTypeDefinition : TypeDefinition
    {
        public RestrictedTypeDefinition(string typeid, ITypeDefinition type, List<ITypeDefinition> restrictions)
        {
            TypeId = typeid;
            Type = type;
            Restrictions = restrictions;
        }

        public override string Kind => "Restriction";

        public string TypeId { get; set; }

        public ITypeDefinition Type { get; set; }

        public List<ITypeDefinition> Restrictions { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var restrictions = RestrictionsAsJson();
            var restrictionsArrayString = $"[{string.Join(",", restrictions)}]";

            return $"{{\"kind\":\"{Kind}\",\"typeID\":\"{TypeId}\",\"type\":{Type.AsJsonCadenceDataFormat()},\"restrictions\":{restrictionsArrayString}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            if (!string.IsNullOrWhiteSpace(TypeId))
                res.Add("typeID", TypeId);

            res.Add("type", Type.Flatten());

            List<dynamic> restrictions = new List<dynamic>();
            foreach (var r in Restrictions)
            {
                restrictions.Add(r.Flatten());
            }
            res.Add("restrictions", restrictions);

            return res;
        }

        public IEnumerable<string> RestrictionsAsJson()
        {
            foreach (var r in Restrictions)
            {
                yield return r.AsJsonCadenceDataFormat();
            }
        }
    }
}
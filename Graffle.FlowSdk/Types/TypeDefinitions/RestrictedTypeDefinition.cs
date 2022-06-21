using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class RestrictedTypeDefinition : TypeDefinition
    {
        public RestrictedTypeDefinition(string typeid, TypeDefinition type, List<TypeDefinition> restrictions)
        {
            TypeId = typeid;
            Type = type;
            Restrictions = restrictions;
        }

        public override string Kind => "Restriction";

        public string TypeId { get; set; }

        public TypeDefinition Type { get; set; }

        public List<TypeDefinition> Restrictions { get; set; }

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
            res.Add("typeID", TypeId);
            res.Add("type", Type.Flatten());

            List<Dictionary<string, dynamic>> restrictions = new List<Dictionary<string, dynamic>>();
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
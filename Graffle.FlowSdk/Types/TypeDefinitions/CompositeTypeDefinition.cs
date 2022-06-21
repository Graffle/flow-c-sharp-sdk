using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class CompositeTypeDefinition : TypeDefinition
    {
        public CompositeTypeDefinition(string kind, string typeId, List<InitializerTypeDefinition> initializers, List<FieldTypeDefinition> fields)
        {
            Kind = kind;
            TypeId = typeId;
            Fields = fields;
            Type = string.Empty; //https://docs.onflow.org/cadence/json-cadence-spec/#composite-types
            Initializers = initializers;
        }

        public override string Kind { get; }

        public string Type { get; set; }

        public string TypeId { get; set; }

        public List<InitializerTypeDefinition> Initializers { get; set; }

        public List<FieldTypeDefinition> Fields { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var fields = FieldsAsJson();
            var fieldsArrayString = $"[{string.Join(",", fields)}]";

            var initializers = InitializersAsJson();
            var initializersArrayString = $"[{string.Join(",", initializers)}]";

            return $"{{\"kind\":\"{Kind}\",\"typeID\":\"{TypeId}\",\"fields\":{fieldsArrayString},\"initializers\":{initializersArrayString},\"type\":\"{Type}\"}}";
        }

        public IEnumerable<string> FieldsAsJson()
        {
            foreach (var field in Fields)
            {
                yield return field.AsJsonCadenceDataFormat();
            }
        }

        public IEnumerable<string> InitializersAsJson()
        {
            foreach (var init in Initializers)
            {
                yield return init.AsJsonCadenceDataFormat();
            }
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("type", Type);
            res.Add("typeID", TypeId);

            var initializers = new List<dynamic>();
            foreach (var init in Initializers)
            {
                initializers.Add(init.Flatten());
            }
            res.Add("initializers", initializers);

            var fields = new List<dynamic>();
            foreach (var field in Fields)
            {
                fields.Add(field.Flatten());
            }

            res.Add("fields", fields);

            return res;
        }
    }
}
using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class EnumTypeDefinition : TypeDefinition
    {
        public EnumTypeDefinition(ITypeDefinition type, string typeId, List<FieldTypeDefinition> fields, List<InitializerTypeDefinition> initializers)
        {
            Type = type;
            TypeId = typeId;
            Fields = fields;
            Initializers = initializers;
        }

        public override string Kind => "Enum";

        public ITypeDefinition Type { get; set; }

        public string TypeId { get; set; }

        public List<FieldTypeDefinition> Fields { get; set; }

        public List<InitializerTypeDefinition> Initializers { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            var fields = FieldsAsJson();
            var fieldsArrayString = $"[{string.Join(",", fields)}]";

            var initializers = InitializersAsJson();
            var initializersArrayString = $"[{string.Join(",", initializers)}]";

            return $"{{\"kind\":\"{Kind}\",\"type\":{Type.AsJsonCadenceDataFormat()},\"typeID\":\"{TypeId}\",\"initializers\":{initializersArrayString},\"fields\":{fieldsArrayString}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("type", Type.Flatten());
            res.Add("typeID", TypeId);

            var flatFields = new List<dynamic>();
            foreach (var f in Fields)
            {
                flatFields.Add(f.Flatten());
            }
            res.Add("fields", flatFields);

            var flatInitializers = new List<dynamic>();
            foreach (var i in Initializers)
            {
                flatInitializers.Add(i.Flatten());
            }
            res.Add("initializers", flatInitializers);

            return res;
        }

        private IEnumerable<string> FieldsAsJson()
        {
            foreach (var f in Fields)
                yield return f.AsJsonCadenceDataFormat();
        }

        private IEnumerable<string> InitializersAsJson()
        {
            foreach (var i in Initializers)
                yield return i.AsJsonCadenceDataFormat();
        }
    }
}
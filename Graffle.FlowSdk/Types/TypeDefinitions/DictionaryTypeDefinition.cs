using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class DictionaryTypeDefinition : TypeDefinition
    {
        public DictionaryTypeDefinition(ITypeDefinition key, ITypeDefinition value)
        {
            Key = key;
            Value = value;
        }

        public override string Kind => "Dictionary";

        public ITypeDefinition Key { get; set; }

        public ITypeDefinition Value { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\",\"key\":{Key.AsJsonCadenceDataFormat()},\"value\":{Value.AsJsonCadenceDataFormat()}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();

            res.Add("kind", Kind);
            res.Add("key", Key.Flatten());
            res.Add("value", Value.Flatten());

            return res;
        }
    }
}
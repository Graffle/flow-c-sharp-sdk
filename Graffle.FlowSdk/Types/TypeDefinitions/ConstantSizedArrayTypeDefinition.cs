using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class ConstantSizedArrayTypeDefinition : TypeDefinition
    {
        public ConstantSizedArrayTypeDefinition(ITypeDefinition type, ulong size)
        {
            Type = type;
            Size = size;
        }

        public override string Kind => "ConstantSizedArray";

        public ITypeDefinition Type { get; set; }

        public ulong Size { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            return $"{{\"kind\":\"{Kind}\",\"size\":{Size},\"type\":{Type}}}";
        }

        public override dynamic Flatten()
        {
            var res = new Dictionary<string, dynamic>();
            res.Add("kind", Kind);
            res.Add("type", Type.Flatten());
            res.Add("size", Size);

            return res;
        }
    }
}
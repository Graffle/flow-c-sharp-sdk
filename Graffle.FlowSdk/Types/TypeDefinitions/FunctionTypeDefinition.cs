using System.Collections.Generic;

namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class FunctionTypeDefinition : TypeDefinition
    {
        public override string Kind => "Function";

        public string TypeId { get; set; }

        public List<ParameterTypeDefinition> Parameters { get; set; }

        public TypeDefinition Return { get; set; }

        public override string AsJsonCadenceDataFormat()
        {
            throw new System.NotImplementedException();
        }

        public override dynamic Flatten()
        {
            throw new System.NotImplementedException();
        }
    }
}
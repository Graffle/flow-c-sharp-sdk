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
    }
}
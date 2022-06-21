namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    public class RepeatedTypeDefinition : ITypeDefinition
    {
        public RepeatedTypeDefinition(string type)
        {
            Type = type;
        }

        public string Type { get; set; }

        public string AsJsonCadenceDataFormat() => $"\"{Type}\"";

        public dynamic Flatten() => Type;
    }
}
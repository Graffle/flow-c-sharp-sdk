namespace Graffle.FlowSdk.Types.TypeDefinitions
{
    /// <summary>
    /// https://docs.onflow.org/cadence/json-cadence-spec/#repeated-types
    /// When a composite type appears more than once in cadence json the type is represented just by its name to save space
    /// eg "restrictions" : [ "A.f233dcee88fe0abe.FungibleToken.Receiver" ] - here we just have a string instead of a json object
    /// </summary>
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
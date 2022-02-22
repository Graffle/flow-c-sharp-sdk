namespace Graffle.FlowSdk.Types
{
    public class CompositeField
    {
        public CompositeField(string name, FlowValueType value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public FlowValueType Value { get; set; }
    }
}
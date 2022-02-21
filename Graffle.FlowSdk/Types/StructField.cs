namespace Graffle.FlowSdk.Types
{
    public class StructField
    {
        public StructField(string name, FlowValueType value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public FlowValueType Value { get; set; }
    }
}
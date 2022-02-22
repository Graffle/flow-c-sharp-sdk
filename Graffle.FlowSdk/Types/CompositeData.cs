using System.Collections.Generic;

namespace Graffle.FlowSdk.Types
{
    public class CompositeData
    {
        public CompositeData(string id, List<CompositeField> fields)
        {
            Id = id;
            Fields = fields;
        }

        public string Id { get; set; }

        public List<CompositeField> Fields { get; set; }
    }
}
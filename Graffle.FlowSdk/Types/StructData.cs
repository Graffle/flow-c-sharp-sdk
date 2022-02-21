using System.Collections.Generic;

namespace Graffle.FlowSdk.Types
{
    public class StructData
    {
        public StructData(string id, List<StructField> fields)
        {
            Id = id;
            Fields = fields;
        }

        public string Id { get; set; }

        public List<StructField> Fields { get; set; }
    }
}
using System.Collections.Generic;

namespace Graffle.FlowSdk {
    public sealed class FlowJson {
        public Dictionary<string, string> Networks { get; set; }
        public Dictionary<string, string> Contracts { get; set; }
        public Dictionary<string, FlowJsonAccount> Accounts { get; set; }
    }
}
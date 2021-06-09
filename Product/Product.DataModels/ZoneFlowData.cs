using System.Collections.Generic;

namespace Product.DataModels
{
    public struct ZoneFlowData
    {
        public int ZoneNumber { get; set; }
        public IReadOnlyCollection<double?> Oil { get; set; }
        public IReadOnlyCollection<double?> Water { get; set; }
        public IReadOnlyCollection<double?> Gas { get; set; }
        public IReadOnlyCollection<long> Time { get; set; }
    }
}
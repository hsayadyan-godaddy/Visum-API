using System.Collections.Generic;
using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct ZonesData
    {
        public DepthType DepthType { get; set; }
        public IReadOnlyCollection<ZoneInfo> Data { get; set; }
    }
}
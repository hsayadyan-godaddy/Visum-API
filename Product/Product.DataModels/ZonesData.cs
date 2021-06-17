using System.Collections.Generic;
using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct ZonesData
    {
        public DepthType DepthType { get; set; }
        public List<ZoneInfo> Data { get; set; }
    }
}
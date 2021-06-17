using System.Collections.Generic;

namespace Product.DataModels
{
    public struct ZoneFlowData
    {
        public int ZoneNumber { get; set; }
        public List<ZoneFlowTimeOilWaterGas> Data { get; set; }
    }
}
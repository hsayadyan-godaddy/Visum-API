using System.Collections.Generic;

namespace Product.DataModels
{
    public struct PressureData
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public List<ValueTime> Data { get; set; }
    }
}
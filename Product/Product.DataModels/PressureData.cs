using System.Collections.Generic;

namespace Product.DataModels
{
    public struct PressureData
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public IReadOnlyCollection<ValueTime> Data { get; set; }
    }
}
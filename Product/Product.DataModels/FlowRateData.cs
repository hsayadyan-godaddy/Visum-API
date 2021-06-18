using System.Collections.Generic;

namespace Product.DataModels
{
    public struct FlowRateData
    {
        public string SensorId { get; set; }
        public List<TimeValue> Data { get; set; }
    }
}
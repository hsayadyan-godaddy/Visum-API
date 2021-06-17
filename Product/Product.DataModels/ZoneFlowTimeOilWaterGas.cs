using Newtonsoft.Json;
using Product.DataModels.Attributes;
using System;

namespace Product.DataModels
{
    public struct ZoneFlowTimeOilWaterGas
    {
        [JsonConverter(typeof(JsonUnixDateFormatConverter))]
        public DateTime Time { get; set; }
        public double Oil { get; set; }
        public double Water { get; set; }
        public double Gas { get; set; }
    }
}
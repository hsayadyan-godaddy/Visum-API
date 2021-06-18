using Newtonsoft.Json;
using Product.DataModels.Attributes;
using System;

namespace Product.DataModels
{
    public struct TimeValue
    {
        [JsonConverter(typeof(JsonUnixDateFormatConverter))]
        public DateTime Time { get; set; }
        public double Value { get; set; }

        public TimeValue(DateTime time, double value)
        {
            Time = time;
            Value = value;
        }
    }
}
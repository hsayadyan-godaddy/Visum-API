using Newtonsoft.Json;
using Product.API.Models.Basics;
using Product.DataModels.Attributes;
using System;

namespace Product.API.Models.ProductionMonitoring
{
    public class ZoneFlowProductionHistoryDataResponse : BaseResponse
    {
        [JsonConverter(typeof(JsonUnixDateFormatConverter))]
        public DateTime VAl { get; set; }
    }

}

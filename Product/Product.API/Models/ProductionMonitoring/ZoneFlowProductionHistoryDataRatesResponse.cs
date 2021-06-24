using Product.API.Models.Basics;
using Product.DataModels;
using Product.DataModels.Enums;
using System.Collections.Generic;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Zone Flow Production History Data response
    /// </summary>
    public class ZoneFlowProductionHistoryDataRatesResponse : BaseResponse
    {
        /// <summary>
        /// Zone number
        /// </summary>
        public int ZoneNumber { get; }
        /// <summary>
        /// Depth type
        /// </summary>
        public DepthType DepthType { get; }
        /// <summary>
        /// Zone info data
        /// </summary>
        public IReadOnlyCollection<ZoneFlowTimeOilWaterGas> ZoneFlowProductionData { get; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public ZoneFlowProductionHistoryDataRatesResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public ZoneFlowProductionHistoryDataRatesResponse(ZoneFlowData value)
        {
            ZoneNumber = value.ZoneNumber;
            ZoneFlowProductionData = value.Data;
        }
    }
}

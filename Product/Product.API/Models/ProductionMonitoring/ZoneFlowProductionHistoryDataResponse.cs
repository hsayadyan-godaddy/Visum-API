using Product.API.Models.Basics;
using Product.DataModels;
using Product.DataModels.Enums;
using System.Collections.Generic;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Zone Flow Production History Data response
    /// </summary>
    public class ZoneFlowProductionHistoryDataResponse : BaseResponse
    {
        /// <summary>
        /// Zone number
        /// </summary>
        public int ZoneNumber { get; }
        /// <summary>
        /// Oil Unit Of Measure Info
        /// </summary>
        public UnitOfMeasureInfo OilUoM { get; }
        /// <summary>
        /// Water Unit Of Measure Info
        /// </summary>
        public UnitOfMeasureInfo WaterUoM { get; }
        /// <summary>
        /// Gas Unit Of Measure Info
        /// </summary>
        public UnitOfMeasureInfo GasUoM { get; }
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
        public ZoneFlowProductionHistoryDataResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        public ZoneFlowProductionHistoryDataResponse(UnitOfMeasureInfo oilUoM, 
                                                     UnitOfMeasureInfo waterUoM, 
                                                     UnitOfMeasureInfo gasUoM,
                                                     ZoneFlowData value)
        {
            ZoneNumber = value.ZoneNumber;
            ZoneFlowProductionData = value.Data;
            OilUoM = oilUoM;
            WaterUoM = waterUoM;
            GasUoM = gasUoM;
        }
    }
}

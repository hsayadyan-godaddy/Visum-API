using Product.API.Models.Basics;
using Product.DataModels;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Flow Rate History Data response
    /// </summary>
    public class FlowRateHistoryDataResponse : BaseResponse
    {
        /// <summary>
        /// UoM of data info
        /// </summary>
        public UnitOfMeasureInfo UnitOfMeasureInfo { get; }
        /// <summary>
        /// Flow Rate data
        /// </summary>
        public FlowRateData FlowRateData { get; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public FlowRateHistoryDataResponse()
        { 
        
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="unitOfMeasureInfo"></param>
        /// <param name="value"></param>
        public FlowRateHistoryDataResponse(UnitOfMeasureInfo unitOfMeasureInfo, FlowRateData value)
        {
            UnitOfMeasureInfo = unitOfMeasureInfo;
            FlowRateData = value;
        }
    }
}

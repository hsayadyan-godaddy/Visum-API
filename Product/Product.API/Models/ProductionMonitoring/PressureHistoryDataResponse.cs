using Product.API.Models.Basics;
using Product.DataModels;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Pressure History Data response
    /// </summary>
    public class PressureHistoryDataResponse : BaseResponse
    {
        /// <summary>
        /// UoM of data info
        /// </summary>
        public UnitOfMeasureInfo UnitOfMeasureInfo { get; }
        /// <summary>
        /// Pressure data
        /// </summary>
        public PressureData PressureData { get; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public PressureHistoryDataResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="unitOfMeasureInfo"></param>
        /// <param name="value"></param>
        public PressureHistoryDataResponse(UnitOfMeasureInfo unitOfMeasureInfo, PressureData value)
        {
            UnitOfMeasureInfo = unitOfMeasureInfo;
            PressureData = value;
        }
    }
}


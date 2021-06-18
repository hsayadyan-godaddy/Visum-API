using Product.API.Models.Basics;
using Product.DataModels;
using Product.DataModels.Enums;
using System.Collections.Generic;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Wellbore Profile Zones response 
    /// </summary>
    public class WellboreProfileZonesResponse : BaseResponse
    {
        /// <summary>
        /// UoM of data info
        /// </summary>
        public UnitOfMeasureInfo UnitOfMeasureInfo { get; }
        /// <summary>
        /// Data depth type
        /// </summary>
        public DepthType DepthType { get; }
        /// <summary>
        /// Zone info data
        /// </summary>
        public IReadOnlyCollection<ZoneInfo> ZoneInfoData { get; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public WellboreProfileZonesResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="unitOfMeasureInfo"></param>
        /// <param name="value"></param>
        public WellboreProfileZonesResponse(UnitOfMeasureInfo unitOfMeasureInfo, ZonesData value)
        {
            UnitOfMeasureInfo = unitOfMeasureInfo;
            DepthType = value.DepthType;
            ZoneInfoData = value.Data;
        }

    }
}

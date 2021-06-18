using Product.API.Models.Basics;
using Product.DataModels;
using System.Collections.Generic;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Flow Rate Sensors Response
    /// </summary>
    public class FlowRateSensorsResponse : BaseResponse
    {
        /// <summary>
        /// Array or sensor info
        /// </summary>
        public IReadOnlyCollection<SensorInfo> Info { get; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public FlowRateSensorsResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="info"></param>
        public FlowRateSensorsResponse(IReadOnlyCollection<SensorInfo> info)
        {
            Info = info;
        }
    }
}

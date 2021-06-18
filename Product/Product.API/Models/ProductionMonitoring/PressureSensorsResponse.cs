using Product.API.Models.Basics;
using Product.DataModels;
using System.Collections.Generic;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Pressure Sensors Response
    /// </summary>
    public class PressureSensorsResponse : BaseResponse
    {
        /// <summary>
        /// Array or sensor info
        /// </summary>
        public IReadOnlyCollection<SensorInfo> Info { get;  }

        /// <summary>
        /// Create new instance
        /// </summary>
        public PressureSensorsResponse()
        { 
        
        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="info"></param>
        public PressureSensorsResponse(IReadOnlyCollection<SensorInfo> info)
        {
            Info = info;
        }
    }
}

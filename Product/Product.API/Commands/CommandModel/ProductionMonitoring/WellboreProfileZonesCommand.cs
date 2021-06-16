using Product.API.Commands.CommandModel.GeneralModels;
using Product.DataModels.Enums;

namespace Product.API.Commands.CommandModel.ProductionMonitoring
{
    /// <summary>
    /// Command/Query Parameters
    /// </summary>
    public class WellboreProfileZonesCommand : ProjectIdWellNameCommand
    {
        /// <summary>
        /// Required Depth type
        /// </summary>
        public DepthType DepthType { get; set; }
    }
}

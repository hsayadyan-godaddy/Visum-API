using Product.API.Commands.CommandModel.GeneralModels;
using Product.DataModels.Enums;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public DepthType DepthType { get; set; }
    }
}

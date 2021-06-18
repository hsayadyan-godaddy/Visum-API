using Product.API.Commands.CommandModel.GeneralModels;
using Product.DataModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Commands.CommandModel.ProductionMonitoring
{
    /// <summary>
    /// Command/Query Parameters
    /// </summary>
    public class ZoneFlowProductionHistoryDataCommand : HistoricaDataCommand
    {
        /// <summary>
        /// Depth type
        /// </summary>
        [Required]
        public DepthType DepthType { get; set; }
        /// <summary>
        /// Zone number
        /// </summary>
        [Required]
        public int ZoneNumber { get; set; }
    }
}


using Product.API.Commands.CommandModel.GeneralModels;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Commands.CommandModel.ProductionMonitoring
{
    /// <summary>
    /// Command/Query Parameters
    /// </summary>
    public class PressureHistoryDataCommand : HistoricalDataCommand
    {
        /// <summary>
        /// Sensor ID for required data
        /// </summary>
        [Required]
        public string SensorId { get; set; }
    }
}

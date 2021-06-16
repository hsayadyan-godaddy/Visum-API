using Product.API.Commands.CommandModel.GeneralModels;


namespace Product.API.Commands.CommandModel.ProductionMonitoring
{
    /// <summary>
    /// Command/Query Parameters
    /// </summary>
    public class PressureHistoryDataCommand : HistoricaDataCommand
    {
        /// <summary>
        /// Key (sensor name) of required data
        /// </summary>
        public string Key { get; set; }
    }
}

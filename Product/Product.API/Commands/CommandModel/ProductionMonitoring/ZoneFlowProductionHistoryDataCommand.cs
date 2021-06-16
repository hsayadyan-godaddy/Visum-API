using Product.API.Commands.CommandModel.GeneralModels;
using Product.DataModels.Attributes;
using Product.DataModels.Enums;
using Product.DataModels.Extensions;
using System;

namespace Product.API.Commands.CommandModel.ProductionMonitoring
{
    /// <summary>
    /// Command/Query Parameters
    /// </summary>
    public class ZoneFlowProductionHistoryDataCommand : HistoricaDataCommand
    {
        /// <summary>
        /// Key (sensor name) of required data
        /// </summary>
        public string Key { get; set; }
       

        public DepthType DepthType { get; }

    }
}


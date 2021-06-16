using Product.DataModels.Attributes;
using Product.DataModels.Extensions;
using System;

namespace Product.API.Commands.CommandModel.GeneralModels
{
    /// <summary>
    /// General implementation of historical data request command
    /// </summary>
    public class HistoricaDataCommand: ProjectIdWellNameCommand
    {
        /// <summary>
        /// From, Unix format 
        /// </summary>
        public long? From { get; set; }
        /// <summary>
        /// To, Unix format 
        /// </summary>
        public long? To { get; set; }

        /// <summary>
        /// From, as DateTime type
        /// </summary>
        [SwaggerExclude]
        public DateTime? NativeFrom => From?.FromUnix();
        /// <summary>
        /// To, as DateTime type
        /// </summary>
        [SwaggerExclude]
        public DateTime? NativeTo => To?.FromUnix();
    }
}

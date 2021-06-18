using Product.API.Attributes;
using Product.DataModels.Enums;
using Product.DataModels.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Product.API.Commands.CommandModel.GeneralModels
{
    /// <summary>
    /// General implementation of historical data request command
    /// </summary>
    public class HistoricaDataCommand : ProjectIdWellNameCommand
    {
        /// <summary>
        /// Required periodicity
        /// </summary>
        [Required]
        public Periodicity Periodicity { get; set; }
        
        /// <summary>
        /// Define size of the snapshot to optimize lenght of data. Will be returned Max-Min points instead all possible data
        /// </summary>
        public long SnapshotSize { get; set; }

        /// <summary>
        /// Date and Time, Unix format 
        /// </summary>
        public long? FromDate { get; set; }
        /// <summary>
        /// Date and Time, Unix format 
        /// </summary>
        public long? ToDate { get; set; }

        /// <summary>
        /// From, as DateTime type
        /// </summary>
        [SwaggerExclude]
        public DateTime? NativeFromDate => FromDate?.FromUnix();
        /// <summary>
        /// To, as DateTime type
        /// </summary>
        [SwaggerExclude]
        public DateTime? NativeToDate => ToDate?.FromUnix();
    }
}

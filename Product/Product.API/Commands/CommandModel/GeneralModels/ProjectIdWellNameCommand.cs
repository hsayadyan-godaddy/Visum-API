using System.ComponentModel.DataAnnotations;

namespace Product.API.Commands.CommandModel.GeneralModels
{
    /// <summary>
    /// Base command
    /// </summary>
    public class ProjectIdWellNameCommand
    {
        /// <summary>
        /// Project identifier
        /// </summary>
        [Required]
        public string ProjectId { get; set; }
        /// <summary>
        /// Well id
        /// </summary>
        [Required]
        public string WellId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Product.API.Commands.CommandModel.GeneralModels
{
    public class ProjectIdWellNameCommand
    {
        /// <summary>
        /// Project identifier
        /// </summary>
        [Required]
        public string ProjectId { get; set; }
        /// <summary>
        /// Well name
        /// </summary>
        [Required]
        public string WellName { get; set; }
    }
}

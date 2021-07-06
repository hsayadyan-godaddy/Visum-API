using System.ComponentModel;

namespace Product.API.Commands.CommandModel.Wellbore
{
    /// <summary>
    /// Well names search 
    /// </summary>
    public class WellboreNamesToCompleteCommand
    {
        /// <summary>
        /// Searching/filtering string
        /// </summary>
        public string SearchString { get; set; }
        /// <summary>
        /// Define if required to return wells connected to specific project. 
        /// </summary>
        public string CurrentProjectId { get; set; }
        /// <summary>
        /// Define max allowed results per page
        /// </summary>
        [DefaultValue(10)]
        public int MaxResults { get; set; }

    }
}

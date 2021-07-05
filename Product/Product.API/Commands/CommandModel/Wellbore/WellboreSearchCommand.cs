using System.ComponentModel;

namespace Product.API.Commands.CommandModel.Wellbore
{
    /// <summary>
    /// Well search 
    /// </summary>
    public class WellboreSearchCommand
    {
        /// <summary>
        /// Searching/filtering string
        /// </summary>
        public string SearchString { get; set; }
        /// <summary>
        /// Define if required to return only nearby wells. 
        /// </summary>
        public bool NearbyWellsOnly { get; set; }
        /// <summary>
        /// Define if required to return only recent wells. 
        /// </summary>
        public bool RecentWells { get; set; }
        /// <summary>
        /// Define if required to return wells connected to specific project. 
        /// </summary>
        public string CurrentProjectId { get; set; }
        /// <summary>
        /// Define max allowed results per page
        /// </summary>
        [DefaultValue(100)]
        public int ResultsPerPage { get; set; }
        /// <summary>
        /// Zero based page index
        /// </summary>
        public int PageIndex { get; set; }

    }
}

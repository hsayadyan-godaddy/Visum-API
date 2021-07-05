using Product.API.Models.Basics;
using Product.DataModels;
using System.Collections.Generic;

namespace Product.API.Models.Wellbore
{
    /// <summary>
    /// Wellbore Profile Zones response 
    /// </summary>
    public class WellboreSearchResponse : BaseResponse
    {
        /// <summary>
        /// Contains information about well and project that has reference to this well
        /// </summary>
        public List<WellboreInfo> Result { get; set; }

        /// <summary>
        /// Zero based page index
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public WellboreSearchResponse()
        {
        }
    }
}

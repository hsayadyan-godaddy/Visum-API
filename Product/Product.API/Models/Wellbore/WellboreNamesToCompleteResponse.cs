using Product.API.Models.Basics;
using System.Collections.Generic;

namespace Product.API.Models.Wellbore
{
    /// <summary>
    /// Wellbore names to complete
    /// </summary>
    public class WellboreNamesToCompleteResponse : BaseResponse
    {
        /// <summary>
        /// Well names
        /// </summary>
        public List<string> Result { get; set; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public WellboreNamesToCompleteResponse()
        {
        }
    }
}

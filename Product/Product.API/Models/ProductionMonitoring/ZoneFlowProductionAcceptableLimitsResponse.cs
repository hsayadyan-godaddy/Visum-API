using Product.API.Models.Basics;
using Product.DataModels;
using Product.DataModels.Basics;

namespace Product.API.Models.ProductionMonitoring
{
    /// <summary>
    /// Zone Flow Production Critical Highlights response
    /// </summary>
    public class ZoneFlowProductionAcceptableLimitsResponse : BaseResponse
    {
        /// <summary>
        /// Acceptable range for Oil flow
        /// </summary>
        public MinMax<double?> Oil { get; set; }
        /// <summary>
        /// Acceptable range for Water flow
        /// </summary>
        public MinMax<double?> Water { get; set; }
        /// <summary>
        /// Acceptable range for Gas flow
        /// </summary>
        public MinMax<double?> Gas { get; set; }

        /// <summary>
        /// Create new instance
        /// </summary>
        public ZoneFlowProductionAcceptableLimitsResponse()
        {

        }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="value"></param>
        public ZoneFlowProductionAcceptableLimitsResponse(FlowAcceptableLimits value)
        {
            Oil = value.Oil;
            Water = value.Water;
            Gas = value.Gas;
        }
    }
}

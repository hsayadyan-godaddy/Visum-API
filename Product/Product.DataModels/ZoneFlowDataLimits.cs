using System.Collections.Generic;

namespace Product.DataModels
{
    public struct ZoneFlowDataLimits
    {
        public IReadOnlyCollection<FlowLimitInfo> FlowLimitInfos { get; set; }
    }
}
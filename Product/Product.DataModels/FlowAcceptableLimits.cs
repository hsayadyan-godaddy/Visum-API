using Product.DataModels.Basics;

namespace Product.DataModels
{
    public struct FlowAcceptableLimits
    {
        public MinMax<double?> Oil { get; set; }
        public MinMax<double?> Water { get; set; }
        public MinMax<double?> Gas { get; set; }
    }
}
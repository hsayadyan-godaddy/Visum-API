namespace Product.DataModels
{
    public struct FlowLimitInfo
    {
        public FlowSource FlowSource { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
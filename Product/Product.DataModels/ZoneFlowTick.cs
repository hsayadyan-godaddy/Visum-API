namespace Product.DataModels
{
    public struct ZoneFlowTick
    {
        public double? Oil { get; set; }
        public double? Water { get; set; }
        public double? Gas { get; set; }
        public long Time { get; set; }
    }
}
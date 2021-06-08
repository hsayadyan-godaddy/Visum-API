using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct UnitOfMeasure
    {
        public SourceToMeasure SourceToMeasure { get; set; }
        public string Label { get; set; }
    }
}
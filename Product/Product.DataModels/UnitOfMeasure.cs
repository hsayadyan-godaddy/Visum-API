using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct UnitOfMeasure
    {
        public SourceType SourceToMeasure { get; set; }
        public string Label { get; set; }
    }
}
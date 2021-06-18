using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct UnitOfMeasureInfo
    {
        public SystemOfUnits SystemOfUnits { get; set; }
        public SourceType SourceToMeasure { get; set; }
        public string Label { get; set; }
    }
}
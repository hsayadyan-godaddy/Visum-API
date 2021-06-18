using Product.DataModels.Enums;

namespace Product.DataModels
{
    public struct SensorInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SourceType SourceType { get; set; }
    }
}

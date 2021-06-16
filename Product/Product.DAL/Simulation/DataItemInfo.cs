using Product.DataModels.Enums;

namespace Product.API.Services.Simulation
{
    public class DataItemInfo
    {
        public string WellName { get; set; }
        public SourceType SourceType { get; set; }
        public string SourceName { get; set; }

        public override string ToString()
        {
            return $"{WellName}-{SourceType}-{SourceName}";
        }
    }
}

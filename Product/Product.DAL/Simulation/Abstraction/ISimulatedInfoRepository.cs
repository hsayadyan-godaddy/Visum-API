using Product.DataModels;
using Product.DataModels.Enums;
using System.Collections.Generic;

namespace Product.DAL.Simulation.Abstraction
{
    public interface ISimulatedInfoRepository
    {
        FlowAcceptableLimits GetFlowAcceptableLimits();
        List<SensorInfo> GetFlowRateInfo();
        List<SensorInfo> GetPressureSensorsInfo();
        UnitOfMeasureInfo GetUnitOfMeasureInfo(SourceType sourceType);
    }
}

using Product.DataModels;
using Product.DataModels.Enums;
using System;

namespace Product.DAL.Simulation.Abstraction
{
    public interface ISimulatedDataRepository
    {
        void FlowRateDataUpdates(string connectionId, string sensorId, Func<TimeValue, bool> callback);
        void PressureDataUpdates(string connectionId, string sensorId, Func<TimeValue, bool> callback);
        void ZoneFlowProductionDataUpdates(string connectionId,
                                           DepthType depthType,
                                           int zoneNumber,
                                           Func<ZoneFlowTimeOilWaterGas, bool> callback);
        ZonesData GetZonesData(DepthType depthType);

        FlowRateData GetFlowRateData(string sensorId,
                                     Periodicity periodicity,
                                     long snapshotSize,
                                     DateTime? fromDate,
                                     DateTime? toDate);

        PressureData GetPressureData(string sensorId,
                                    Periodicity periodicity,
                                    long snapshotSize,
                                    DateTime? fromDate,
                                    DateTime? toDate);

        ZoneFlowData GetZoneFlowProductionData(DepthType depthType,
                                               int zoneNumber,
                                               Periodicity periodicity,
                                               long snapshotSize,
                                               DateTime? fromDate,
                                               DateTime? toDate);



    }
}

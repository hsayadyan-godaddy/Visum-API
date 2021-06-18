using Product.DataModels;
using Product.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services.ProductionMonitoring
{
    public interface IProductionMonitoringService
    {
        void PressureDataUpdates(string connectionId,  
                                 string projectId, 
                                 string wellId, 
                                 string sensorId, 
                                 Func<TimeValue, bool> callback);
        void FlowRateDataUpdates(string connectionId, 
                                 string projectId, 
                                 string wellId, 
                                 string sensorId, 
                                 Func<TimeValue, bool> callback);
        void ZoneFlowProductionDataUpdates(string connectionId, 
                                           string projectId, 
                                           string wellId, 
                                           DepthType depthType,
                                           int zoneNumber, 
                                           Func<ZoneFlowTimeOilWaterGas, bool> callback);

        Task<List<SensorInfo>> GetPressureSensorsInfoAsync(string projectId, string wellId);
        Task<List<SensorInfo>> GetFlowRateInfoAsync(string projectId, string wellId);
        Task<ZonesData> GetZonesDataAsync(string projectId, string wellId, DepthType depthType);
        Task<UnitOfMeasureInfo> GetUnitOfMeasureInfoAsync(string projectId, string wellId, SourceType sourceType);
        Task<FlowAcceptableLimits> GetFlowAcceptableLimitsAsync(string projectId, string wellId);
        Task<PressureData> GetPressureDataAsync(string projectId,
                                                string wellId,
                                                string sensorId,
                                                Periodicity periodicity,
                                                long snapshotSize,
                                                DateTime? fromDate,
                                                DateTime? toDate);
        Task<FlowRateData> GetFlowRateDataAsync(string projectId,
                                                string wellId,
                                                string sensorId,
                                                Periodicity periodicity,
                                                long snapshotSize,
                                                DateTime? fromDate,
                                                DateTime? toDate);
        Task<ZoneFlowData> GetZoneFlowProductionDataAsync(string projectId,
                                                string wellId,
                                                DepthType depthType,
                                                int zoneNumber,
                                                Periodicity periodicity,
                                                long snapshotSize,
                                                DateTime? fromDate,
                                                DateTime? toDate);
    }
}

using Product.DAL.Simulation.Abstraction;
using Product.DataModels;
using Product.DataModels.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Services.ProductionMonitoring
{
    internal class ProductionMonitoringService : IProductionMonitoringService
    {
        #region members

        private readonly ISimulatedDataRepository _simulatedDataRepository;
        private readonly ISimulatedInfoRepository _simulatedInfoRepository;

        #endregion

        #region ctor

        public ProductionMonitoringService(ISimulatedDataRepository simulatedDataRepository,
                                           ISimulatedInfoRepository simulatedInfoRepository)
        {
            _simulatedDataRepository = simulatedDataRepository;
            _simulatedInfoRepository = simulatedInfoRepository;
        }

        #endregion

        #region publics

        public void FlowRateDataUpdates(string connectionId,
                                        string projectId,
                                        string wellId,
                                        string sensorId,
                                        Func<TimeValue, bool> callback)
        {
            _simulatedDataRepository.FlowRateDataUpdates(connectionId, sensorId, callback);
        }

        public Task<FlowAcceptableLimits> GetFlowAcceptableLimitsAsync(string projectId, string wellId)
        {
            return Task.FromResult(_simulatedInfoRepository.GetFlowAcceptableLimits());
        }

        public Task<FlowRateData> GetFlowRateDataAsync(string projectId,
                                                       string wellId,
                                                       string sensorId,
                                                       Periodicity periodicity,
                                                       long snapshotSize,
                                                       DateTime? fromDate,
                                                       DateTime? toDate)
        {
            return Task.FromResult(_simulatedDataRepository.GetFlowRateData(sensorId,
                                                                            periodicity,
                                                                            snapshotSize,
                                                                            fromDate,
                                                                            toDate));
        }

        public Task<List<SensorInfo>> GetFlowRateInfoAsync(string projectId, string wellId)
        {
            return Task.FromResult(_simulatedInfoRepository.GetFlowRateInfo());
        }

        public Task<PressureData> GetPressureDataAsync(string projectId,
                                                       string wellId,
                                                       string sensorId,
                                                       Periodicity periodicity,
                                                       long snapshotSize, DateTime? fromDate,
                                                       DateTime? toDate)
        {
            return Task.FromResult(_simulatedDataRepository.GetPressureData(sensorId,
                                                                            periodicity,
                                                                            snapshotSize,
                                                                            fromDate,
                                                                            toDate));
        }

        public Task<List<SensorInfo>> GetPressureSensorsInfoAsync(string projectId, string wellId)
        {
            return Task.FromResult(_simulatedInfoRepository.GetPressureSensorsInfo());
        }

        public Task<UnitOfMeasureInfo> GetUnitOfMeasureInfoAsync(string projectId, string wellId, SourceType sourceType)
        {
            return Task.FromResult(_simulatedInfoRepository.GetUnitOfMeasureInfo(sourceType));
        }

        public Task<ZoneFlowData> GetZoneFlowProductionDataAsync(string projectId,
                                                                 string wellId,
                                                                 DepthType depthType,
                                                                 int zoneNumber,
                                                                 Periodicity periodicity,
                                                                 long snapshotSize,
                                                                 DateTime? fromDate,
                                                                 DateTime? toDate)
        {
            return Task.FromResult(_simulatedDataRepository.GetZoneFlowProductionData(false, depthType,
                                                                                      zoneNumber,
                                                                                      periodicity,
                                                                                      snapshotSize,
                                                                                      fromDate,
                                                                                      toDate));
        }

        public Task<ZoneFlowData> GetZoneFlowProductionDataRatesAsync(string projectId,
                                                                      string wellId,
                                                                      DepthType depthType,
                                                                      int zoneNumber,
                                                                      Periodicity periodicity,
                                                                      long snapshotSize,
                                                                      DateTime? fromDate,
                                                                      DateTime? toDate)
        {
            return Task.FromResult(_simulatedDataRepository.GetZoneFlowProductionData(true, depthType,
                                                                                       zoneNumber,
                                                                                       periodicity,
                                                                                       snapshotSize,
                                                                                       fromDate,
                                                                                       toDate));
        }

        public Task<ZonesData> GetZonesDataAsync(string projectId, string wellId, DepthType depthType)
        {
            return Task.FromResult(_simulatedDataRepository.GetZonesData(depthType));
        }

        public void PressureDataUpdates(string connectionId,
                                        string projectId,
                                        string wellId,
                                        string sensorId,
                                        Func<TimeValue, bool> callback)
        {
            _simulatedDataRepository.PressureDataUpdates(connectionId, sensorId, callback);
        }

        public void ZoneFlowProductionDataUpdates(bool returnRates,
                                                  string connectionId,
                                                  string projectId,
                                                  string wellId,
                                                  DepthType depthType,
                                                  int zoneNumber,
                                                  Func<ZoneFlowTimeOilWaterGas, bool> callback)
        {
            _simulatedDataRepository.ZoneFlowProductionDataUpdates(returnRates,
                                                                   connectionId,
                                                                   depthType,
                                                                   zoneNumber,
                                                                   callback);
        }

        #endregion
    }
}

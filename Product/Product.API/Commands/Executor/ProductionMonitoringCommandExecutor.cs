using Microsoft.AspNetCore.Http;
using Product.API.Commands.Abstraction;
using Product.API.Commands.CommandModel.ProductionMonitoring;
using Product.API.Models.ProductionMonitoring;
using Product.DataModels.Enums;
using Product.Services.ProductionMonitoring;
using System;
using System.Threading.Tasks;

namespace Product.API.Commands.Executor
{
    /// <summary>
    /// Production Monitoring CommandExecutor
    /// </summary>
    public class ProductionMonitoringCommandExecutor : IAsyncCommandExecutor<WellboreProfileZonesCommand, WellboreProfileZonesResponse>,
                                                       IAsyncCommandExecutor<PressureSensorsCommand, PressureSensorsResponse>,
                                                       IAsyncCommandExecutor<FlowRateSensorsCommand, FlowRateSensorsResponse>,
                                                       IAsyncCommandExecutor<PressureHistoryDataCommand, PressureHistoryDataResponse>,
                                                       IAsyncCommandExecutor<FlowRateHistoryDataCommand, FlowRateHistoryDataResponse>,
                                                       IAsyncCommandExecutor<ZoneFlowProductionHistoryDataCommand, ZoneFlowProductionHistoryDataResponse>,
                                                       IAsyncCommandExecutor<ZoneFlowProductionHistoryDataRatesCommand, ZoneFlowProductionHistoryDataRatesResponse>,
                                                       IAsyncCommandExecutor<ZoneFlowProductionAcceptableLimitsCommand, ZoneFlowProductionAcceptableLimitsResponse>

    {
        #region members

        private readonly IProductionMonitoringService _productionMonitoringService;

        #endregion

        #region ctor

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="productionMonitoringService"></param>
        public ProductionMonitoringCommandExecutor(IProductionMonitoringService productionMonitoringService)
        {
            _productionMonitoringService = productionMonitoringService;
        }

        #endregion

        #region publics

        /// <summary>
        /// Get Pressure Sensors Info
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<PressureSensorsResponse> ExecuteAsync(PressureSensorsCommand command, HttpContext context)
        {
            var result = await _productionMonitoringService.GetPressureSensorsInfoAsync(command.ProjectId, command.WellId);
            return new PressureSensorsResponse(result);
        }

        /// <summary>
        /// Get Wellbore Profile zones
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<WellboreProfileZonesResponse> ExecuteAsync(WellboreProfileZonesCommand command, HttpContext context)
        {
            var uomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.Depth);
            var result = await _productionMonitoringService.GetZonesDataAsync(command.ProjectId, command.WellId, command.DepthType);
            
            return new WellboreProfileZonesResponse(uomInfo, result);
        }

        /// <summary>
        /// Get Flow Rate Sensors Info
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<FlowRateSensorsResponse> ExecuteAsync(FlowRateSensorsCommand command, HttpContext context)
        {
            var result = await _productionMonitoringService.GetFlowRateInfoAsync(command.ProjectId, command.WellId);
            return new FlowRateSensorsResponse(result);
        }

        /// <summary>
        /// Get Flow acceptable limitss
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<ZoneFlowProductionAcceptableLimitsResponse> ExecuteAsync(ZoneFlowProductionAcceptableLimitsCommand command, HttpContext context)
        {
            var result = await _productionMonitoringService.GetFlowAcceptableLimitsAsync(command.ProjectId, command.WellId);
            return new ZoneFlowProductionAcceptableLimitsResponse(result);
        }

        /// <summary>
        /// Get Pressure History data
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<PressureHistoryDataResponse> ExecuteAsync(PressureHistoryDataCommand command, HttpContext context)
        {
            var uomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.Pressure);
            var result = await _productionMonitoringService.GetPressureDataAsync(command.ProjectId, 
                                                                                 command.WellId, 
                                                                                 command.SensorId, 
                                                                                 command.Periodicity, 
                                                                                 command.SnapshotSize, 
                                                                                 command.NativeFromDate, 
                                                                                 command.NativeToDate);

            return new PressureHistoryDataResponse(uomInfo, result);
        }

        /// <summary>
        /// Get FlowRate History data
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<FlowRateHistoryDataResponse> ExecuteAsync(FlowRateHistoryDataCommand command, HttpContext context)
        {
            var uomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.FlowRate);
            var result = await _productionMonitoringService.GetFlowRateDataAsync(command.ProjectId,
                                                                                 command.WellId,
                                                                                 command.SensorId,
                                                                                 command.Periodicity,
                                                                                 command.SnapshotSize,
                                                                                 command.NativeFromDate,
                                                                                 command.NativeToDate);

            return new FlowRateHistoryDataResponse(uomInfo, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<ZoneFlowProductionHistoryDataResponse> ExecuteAsync(ZoneFlowProductionHistoryDataCommand command, HttpContext context)
        {
            var oilUomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.Oil);
            var waterUomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.Water);
            var gasUomInfo = await _productionMonitoringService.GetUnitOfMeasureInfoAsync(command.ProjectId, command.WellId, SourceType.Gas);

            var result = await _productionMonitoringService.GetZoneFlowProductionDataAsync(command.ProjectId,
                                                                                 command.WellId,
                                                                                 command.DepthType,
                                                                                 command.ZoneNumber,
                                                                                 command.Periodicity,
                                                                                 command.SnapshotSize,
                                                                                 command.NativeFromDate,
                                                                                 command.NativeToDate);

            return new ZoneFlowProductionHistoryDataResponse(oilUomInfo,waterUomInfo, gasUomInfo, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<ZoneFlowProductionHistoryDataRatesResponse> ExecuteAsync(ZoneFlowProductionHistoryDataRatesCommand command, HttpContext context)
        {
            var result = await _productionMonitoringService.GetZoneFlowProductionDataRatesAsync(command.ProjectId,
                                                                                 command.WellId,
                                                                                 command.DepthType,
                                                                                 command.ZoneNumber,
                                                                                 command.Periodicity,
                                                                                 command.SnapshotSize,
                                                                                 command.NativeFromDate,
                                                                                 command.NativeToDate);

            return new ZoneFlowProductionHistoryDataRatesResponse(result);
        }


        


        #endregion
    }

}

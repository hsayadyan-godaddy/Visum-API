using Microsoft.AspNetCore.Http;
using Product.API.Commands.Abstraction;
using Product.API.Commands.CommandModel.ProductionMonitoring;
using Product.API.Models.ProductionMonitoring;
using Product.Services.ProductionMonitoring;
using System;
using System.Threading.Tasks;

namespace Product.API.Commands.Executor
{
    public class ProductionMonitoringCommandExecutor : IAsyncCommandExecutor<PressureKeysCommand, PressureKeysResponse>,
                                                       IAsyncCommandExecutor<WellboreProfileZonesCommand, WellboreProfileZonesResponse>,
                                                       IAsyncCommandExecutor<FlowRateKeysCommand, FlowRateKeysResponse>,
                                                       IAsyncCommandExecutor<PressureHistoryDataCommand, PressureHistoryDataResponse>,
                                                       IAsyncCommandExecutor<FlowRateHistoryDataCommand, FlowRateHistoryDataResponse>,
                                                       IAsyncCommandExecutor<ZoneFlowProductionHistoryDataCommand, ZoneFlowProductionHistoryDataResponse>,
                                                       IAsyncCommandExecutor<ZoneFlowProductionCriticalHighlightsCommand, ZoneFlowProductionCriticalHighlightsResponse>

        


    {
        #region members

        private readonly IProductionMonitoringService _productionMonitoringService;

        #endregion

        #region ctor

        public ProductionMonitoringCommandExecutor(IProductionMonitoringService productionMonitoringService)
        {
            _productionMonitoringService = productionMonitoringService;
        }

        #endregion

        #region publics

        public Task<PressureKeysResponse> ExecuteAsync(PressureKeysCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<WellboreProfileZonesResponse> ExecuteAsync(WellboreProfileZonesCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<FlowRateKeysResponse> ExecuteAsync(FlowRateKeysCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<PressureHistoryDataResponse> ExecuteAsync(PressureHistoryDataCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<FlowRateHistoryDataResponse> ExecuteAsync(FlowRateHistoryDataCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneFlowProductionHistoryDataResponse> ExecuteAsync(ZoneFlowProductionHistoryDataCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task<ZoneFlowProductionCriticalHighlightsResponse> ExecuteAsync(ZoneFlowProductionCriticalHighlightsCommand command, HttpContext context)
        {
            throw new NotImplementedException();
        }

        //public async Task<AccountPropertiesResponse> ExecuteAsync(ProductionMonitoringCommand command, HttpContext context)
        //{
        //    var info = UserSession(context);
        //    var request = command.ToPipeRequest(info);

        //    var result = await _nextGenPipe.RequestAsync(request, info.SessionToken);
        //    return new AccountPropertiesResponse(result);
        //}

        #endregion
    }

}

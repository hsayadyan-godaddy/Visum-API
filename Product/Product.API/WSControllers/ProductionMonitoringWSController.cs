using Product.API.WebSocketAPI.Basics;
using Product.API.WebSocketAPI.CustomAttributes;
using Product.DataModels;
using Product.DataModels.Enums;
using Product.Services.ProductionMonitoring;
using System;

namespace Product.API.WSControllers
{
    /// <summary>
    /// Production Monitoring WebSocket operation controller
    /// </summary>
    [WSController]
    public class ProductionMonitoringWSController
    {
        #region members

        private readonly IProductionMonitoringService _productionMonitoringService;

        #endregion

        #region ctor

        /// <summary>
        /// Create new instance
        /// </summary>
        public ProductionMonitoringWSController(IProductionMonitoringService productionMonitoringService)
        {
            _productionMonitoringService = productionMonitoringService;
        }

        #endregion

        #region publics

        /// <summary>
        /// Call Pressure data updates
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="wellId"></param>
        /// <param name="sensorId"></param>
        /// <param name="context"></param>
        [WSMethod(typeof(TimeValue))]
        public void PressureDataUpdates(string projectId, string wellId, string sensorId, WSContext context)
        {
            var refCtx = context;

            Func<TimeValue, bool> dataCallback = null;

            if (context.RequestType == WSRequestType.Subscribe)
            {
                dataCallback = (data) =>
                {
                    var success = refCtx.ResultCallback(new OperationResponse(data,
                                                                 OperationResponseStatus.OK));
                    return success;
                };
            }

            _productionMonitoringService.PressureDataUpdates(context.ConnectionId, 
                                                             projectId, 
                                                             wellId, 
                                                             sensorId, 
                                                             dataCallback);
        }

        /// <summary>
        /// Call Flow Rate data updates
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="wellId"></param>
        /// <param name="sensorId"></param>
        /// <param name="context"></param>
        [WSMethod(typeof(TimeValue))]
        public void FlowRateDataUpdates(string projectId, 
                                        string wellId, 
                                        string sensorId, 
                                        WSContext context)
        {
            var refCtx = context;

            Func<TimeValue, bool> dataCallback = null;

            if (context.RequestType == WSRequestType.Subscribe)
            {
                dataCallback = (data) =>
                {
                    var success = refCtx.ResultCallback(new OperationResponse(data,
                                                                 OperationResponseStatus.OK));
                    return success;
                };
            }

            _productionMonitoringService.FlowRateDataUpdates(context.ConnectionId, 
                                                             projectId, 
                                                             wellId, 
                                                             sensorId, 
                                                             dataCallback);
        }

        /// <summary>
        /// Call Zone Flow Production data updates
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="wellId"></param>
        /// <param name="depthType"></param>
        /// <param name="zoneNumber"></param>
        /// <param name="context"></param>
        [WSMethod(typeof(ZoneFlowTimeOilWaterGas))]
        public void ZoneFlowProductionDataUpdates(string projectId,
                                                  string wellId,
                                                  DepthType depthType,
                                                  int zoneNumber,
                                                  WSContext context)
        {
            var refCtx = context;


            Func<ZoneFlowTimeOilWaterGas, bool> dataCallback = null;

            if (context.RequestType == WSRequestType.Subscribe)
            {
                dataCallback = (data) =>
                {
                    var success = refCtx.ResultCallback(new OperationResponse(data,
                                                             OperationResponseStatus.OK));
                    return success;
                };
            }

            _productionMonitoringService.ZoneFlowProductionDataUpdates(false,
                                                                       context.ConnectionId, 
                                                                       projectId, 
                                                                       wellId, 
                                                                       depthType, 
                                                                       zoneNumber, 
                                                                       dataCallback);
        }

        /// <summary>
        /// Call Zone Flow Production rates data updates
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="wellId"></param>
        /// <param name="depthType"></param>
        /// <param name="zoneNumber"></param>
        /// <param name="context"></param>
        [WSMethod(typeof(ZoneFlowTimeOilWaterGas))]
        public void ZoneFlowProductionDataRatesUpdates(string projectId,
                                                  string wellId,
                                                  DepthType depthType,
                                                  int zoneNumber,
                                                  WSContext context)
        {
            var refCtx = context;


            Func<ZoneFlowTimeOilWaterGas, bool> dataCallback = null;

            if (context.RequestType == WSRequestType.Subscribe)
            {
                dataCallback = (data) =>
                {
                    var success = refCtx.ResultCallback(new OperationResponse(data,
                                                             OperationResponseStatus.OK));
                    return success;
                };
            }

            _productionMonitoringService.ZoneFlowProductionDataUpdates(true,
                                                                       context.ConnectionId,
                                                                       projectId,
                                                                       wellId,
                                                                       depthType,
                                                                       zoneNumber,
                                                                       dataCallback);
        }

        #endregion
    }
}

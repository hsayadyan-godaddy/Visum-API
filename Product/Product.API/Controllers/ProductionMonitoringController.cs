using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Attributes;
using Product.API.Commands.CommandModel.ProductionMonitoring;
using Product.API.Commands.Executor;
using Product.API.Models.Basics;
using Product.API.Models.Error;
using Product.API.Models.ProductionMonitoring;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Product.API
{
    /// <summary>
    /// Production Monitoring API endpoint
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ModelValidation]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status500InternalServerError)]
    public class ProductionMonitoringController : ControllerBase
    {
        #region members

        private readonly ProductionMonitoringCommandExecutor _commandExecutor;

        #endregion

        #region ctor

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="commandExecutor"></param>
        public ProductionMonitoringController(ProductionMonitoringCommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        #endregion

        #region publics

        /// <summary>
        /// Get Zones for the Wellbore Profile
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Zones and depth ranges for specified well</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WellboreProfileZonesResponse), StatusCodes.Status200OK)]
        [Route("wellboreProfile/zones")]
        public async Task<IActionResult> GetWellboreProfileZones([FromQuery] WellboreProfileZonesCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        /// Get available keys (sensors) for the Pressure data
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of strings/keys</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PressureKeysResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/pressure/keys")]
        public async Task<IActionResult> GetPressureKeys([FromQuery] PressureKeysCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        ///  Get available keys (sensors) for the Flow Rate data
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of strings/keys</returns>
        [HttpGet]
        [ProducesResponseType(typeof(FlowRateKeysResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/flowRate/keys")]
        public async Task<IActionResult> GetFlowRateKeys([FromQuery] FlowRateKeysCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        ///  Get historical Pressure data
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of Values and Dates structure</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PressureHistoryDataResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/pressure/historyData")]
        public async Task<IActionResult> GetPressureHistoryData([FromQuery] PressureHistoryDataCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        /// Get historical Flow Rate data
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of Values and Dates structure</returns>
        [HttpGet]
        [ProducesResponseType(typeof(FlowRateHistoryDataResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/flowRate/historyData")]
        public async Task<IActionResult> GetFlowRateHistoryData([FromQuery] FlowRateHistoryDataCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        /// Get historica data for Zone Flow Production
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of Values and Dates structure that contains data for oil, water and gas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ZoneFlowProductionHistoryDataResponse), StatusCodes.Status200OK)]
        [Route("zoneFlowProduction/historyData")]
        public async Task<IActionResult> GetZoneFlowProductionHistoryData([FromQuery] ZoneFlowProductionHistoryDataCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        /// <summary>
        /// Get Critical limits to Highlight zones
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array with limits that should be highlighted</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ZoneFlowProductionCriticalHighlightsResponse), StatusCodes.Status200OK)]
        [Route("zoneFlowProduction/criticalHighlights")]
        public async Task<IActionResult> GetZoneFlowProductionCriticalHighlights([FromQuery] ZoneFlowProductionCriticalHighlightsCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

#endregion

        #region privates

        private IActionResult HandleResult(IBaseResponse result)
        {
            if (result.Error == null)
            {
                return Ok(result);
            }
            else
            {
                return new ObjectResult(result.Error)
                {
                    StatusCode = result.Error.ErrorCode
                };
            }
        }

        #endregion


        /*

         

           */




        /*


        [HttpPost]
        [Route("pressureFlowRate/flowallocation")]
        public async Task<ActionResult> GetZones([FromBody] ZonesQuery zonesQuery)
        {
            var zoneFlowData = _productionMonitoringService.GetZones(zonesQuery);
            return Ok(zoneFlowData);
        }






        [HttpGet]
        [Route("uom")]
        public async Task<ActionResult> GetUom()
        {
            var wellData = _productionMonitoringService.GetUom();
            return Ok(wellData);
        }

        [HttpGet]
        [Route("zones/flowallocation/limits")]
        public async Task<ActionResult> GetLimits()
        {
            var flowLimitInfo = _productionMonitoringService.GetLimits();
            return Ok(flowLimitInfo);
        }

        [HttpGet]
        [Route("zones/{wellName}")]
        public async Task<ActionResult> GetZones(string wellName)
        {
            var zonesData = _productionMonitoringService.GetZones(wellName);
            return Ok(zonesData);
        }

        [HttpPost]
        [Route("zones/flowallocation")]
        public async Task<ActionResult> GetZones([FromBody] ZonesQuery zonesQuery)
        {
            var zoneFlowData = _productionMonitoringService.GetZones(zonesQuery);
            return Ok(zoneFlowData);
        }

        [HttpGet]
        [Route("zones/pressure/{wellName}")]
        public async Task<ActionResult> GetPressure(string wellName)
        {
            var wellData = _productionMonitoringService.GetPressure(wellName);
            return Ok(wellData);
        }

        [HttpGet]
        [Route("pressure/rates/{wellName}/{key}")]
        public async Task<ActionResult> GetPressureRates(string wellName, string key)
        {
            var pressure = _productionMonitoringService.GetPressureRates(wellName, key);
            return Ok(pressure);
        }

        [HttpGet]
        [Route("flow/{wellName}")]
        public async Task<ActionResult> GetFlow(string wellName)
        {
            var wellData = _productionMonitoringService.GetFlow(wellName);
            return Ok(wellData);
        }

        [HttpGet]
        [Route("flow/rates/{wellName}/{key}")]
        public async Task<ActionResult> GetFlow(string wellName, string key)
        {
            var wellData = _productionMonitoringService.GetFlowRates(wellName, key);
            return Ok(wellData);
        }

        */

    }
}

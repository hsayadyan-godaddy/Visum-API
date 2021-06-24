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
        [ProducesResponseType(typeof(PressureSensorsResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/pressure/sensors")]
        public async Task<IActionResult> GetPressureSensors([FromQuery] PressureSensorsCommand value)
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
        [ProducesResponseType(typeof(FlowRateSensorsResponse), StatusCodes.Status200OK)]
        [Route("pressureFlowRate/flowRate/sensors")]
        public async Task<IActionResult> GetFlowRateSensors([FromQuery] FlowRateSensorsCommand value)
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
        /// Get historica data for Zone Flow Production
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Array of Values and Dates structure that contains data for oil, water and gas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ZoneFlowProductionHistoryDataRatesResponse), StatusCodes.Status200OK)]
        [Route("zoneFlowProduction/historyData/rates")]
        public async Task<IActionResult> GetZoneFlowProductionHistoryDataRates([FromQuery] ZoneFlowProductionHistoryDataRatesCommand value)
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
        [ProducesResponseType(typeof(ZoneFlowProductionAcceptableLimitsResponse), StatusCodes.Status200OK)]
        [Route("zoneFlowProduction/criticalHighlights")]
        public async Task<IActionResult> GetZoneFlowProductionAcceptableLimits([FromQuery] ZoneFlowProductionAcceptableLimitsCommand value)
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
    }
}

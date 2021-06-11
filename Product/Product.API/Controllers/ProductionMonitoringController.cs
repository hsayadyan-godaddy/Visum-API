using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.API.Queries;
using Product.API.Services;

namespace Product.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionMonitoringController : ControllerBase
    {
        private readonly IProductionMonitoringService _productionMonitoringService;

        public ProductionMonitoringController(IProductionMonitoringService productionMonitoringService)
        {
            _productionMonitoringService = productionMonitoringService;
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

    }
}

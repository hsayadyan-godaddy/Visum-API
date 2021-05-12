using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using VisumAPI.Models;

namespace VisumAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WellController : ControllerBase
    {
        //TODO should be moved to service or DB project
        private readonly DBClient _dbClient;

        public WellController(DBClient dBClient)
        {
            _dbClient = dBClient;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetWells(string id)
        {
            var wells =  await _dbClient.GetWellsByProjectId(id);
            var jsonResult = JsonConvert.SerializeObject(wells);
            return Ok(jsonResult);
        }


        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddWell([FromBody] Well well)
        {
            await _dbClient.AddWell(well);
            return Created("", well);
        }

        [HttpGet]
        [Route("well/{id}")]
        public async Task<ActionResult> GetWell(string id)
        {
            var well = await _dbClient.GetWellById(id);
            var jsonResult = JsonConvert.SerializeObject(well);
            return Ok(jsonResult);
        }
    }
}

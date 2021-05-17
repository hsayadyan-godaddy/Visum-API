using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisumAPI.Models;
using VisumData;

namespace VisumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        //TODO should be moved to service or DB project
        private readonly DBClient _dbClient;

        public DataController(DBClient dBClient)
        {
            _dbClient = dBClient;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddProject([FromBody] WellData data)
        {
            await _dbClient.AddWellData(data);
            return Ok();
        }
    }
}

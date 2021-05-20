using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<ActionResult> AddWllData([FromBody] WellData data)
        {
            await _dbClient.AddWellData(data);
            return Ok(data);
        }
    }
}

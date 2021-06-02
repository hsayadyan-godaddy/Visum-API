using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.DataModels;


namespace Product.API.Controllers
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


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetData(string id)
        {
            var wellData = await _dbClient.GetWellDataById(id);
            var jsonResult = JsonConvert.SerializeObject(wellData);
            return Ok(jsonResult);
        }
    }
}

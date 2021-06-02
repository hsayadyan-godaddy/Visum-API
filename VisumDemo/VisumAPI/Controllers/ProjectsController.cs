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
    public class ProjectsController : ControllerBase
    {
        //TODO should be moved to service or DB project
        private readonly DBClient _dbClient;

        public ProjectsController(DBClient dBClient)
        {
            _dbClient = dBClient;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetProjects()
        {
            var userId = ObjectId.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var projects =  await _dbClient.GetProjectsForUser(userId);
            var jsonResult = JsonConvert.SerializeObject(projects);
            return Ok(jsonResult);
        }


        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddProject([FromBody] Project project)
        {

            var userId = MongoDB.Bson.ObjectId.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _dbClient.AddProject(project, userId.ToString());
            return Created("", project);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetProject(string id)
        {
            var project = await _dbClient.GetProjectById(id);
            var jsonResult = JsonConvert.SerializeObject(project);
            return Ok(jsonResult);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Route("projects")]
        public async Task<ActionResult> GetProjects()
        {
            return new JsonResult("OK");
        }
    }
}

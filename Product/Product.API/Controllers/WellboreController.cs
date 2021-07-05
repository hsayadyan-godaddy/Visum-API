using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Attributes;
using Product.API.Commands.CommandModel.Wellbore;
using Product.API.Commands.Executor;
using Product.API.Controllers.Basics;
using Product.API.Models.Error;
using Product.API.Models.Wellbore;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    /// <summary>
    /// Wellbore data API endpoint
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ModelValidation]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServerError), StatusCodes.Status500InternalServerError)]
    public class WellboreController : RestAPIController
    {
        #region members

        private readonly WellboreCommandExecutor _commandExecutor;

        #endregion

        #region ctor

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="commandExecutor"></param>
        public WellboreController(WellboreCommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        #endregion

        #region publics

        /// <summary>
        /// Get available wellbores
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Zones and depth ranges for specified well</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WellboreSearchResponse), StatusCodes.Status200OK)]
        [Route("search")]
        public async Task<IActionResult> GetWellbores([FromQuery] WellboreSearchCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }


        /// <summary>
        /// Get wellbore names for autocomplete
        /// </summary>
        /// <param name="value">Request parameters</param>
        /// <returns>Zones and depth ranges for specified well</returns>
        [HttpGet]
        [ProducesResponseType(typeof(WellboreNamesToCompleteResponse), StatusCodes.Status200OK)]
        [Route("search/wellnames")]
        public async Task<IActionResult> GetWellboreNames([FromQuery] WellboreNamesToCompleteCommand value)
        {
            var result = await _commandExecutor.ExecuteAsync(value, HttpContext);
            return HandleResult(result);
        }

        #endregion
    }
}

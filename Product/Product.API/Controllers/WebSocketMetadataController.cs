using Microsoft.AspNetCore.Mvc;
using Product.API.WebSocketAPI.Abstraction;
using Product.API.WebSocketAPI.Basics;
using System.Collections.Generic;

namespace Product.API.Controllers
{
    /// <summary>
    /// WebSocket Metadata
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketMetadataController : ControllerBase
    {
        private readonly IOperationExecutor _operationExecutor;

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <param name="operationExecutor"></param>
        public WebSocketMetadataController(IOperationExecutor operationExecutor)
        {
            _operationExecutor = operationExecutor;
        }

        /// <summary>
        /// Get data models metadata
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("knownModels")]
        [ProducesResponseType(typeof(List<string>), 200)]
        public ActionResult GetDataModels()
        {
            var datamodels = _operationExecutor.KnownModels;
            return Ok(datamodels);
        }

        /// <summary>
        /// Get operations models metadata
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("operationsMetadata")]
        [ProducesResponseType(typeof(List<OperationMetadata>), 200)]
        public ActionResult GetOperationsMetadata()
        {
            var datamodels = _operationExecutor.SupportedOperations;
            return Ok(datamodels);
        }

    }
}

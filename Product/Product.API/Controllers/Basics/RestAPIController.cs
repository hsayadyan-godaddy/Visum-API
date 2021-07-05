using Microsoft.AspNetCore.Mvc;
using Product.API.Models.Basics;

namespace Product.API.Controllers.Basics
{
    /// <summary>
    /// Basic controller implementation
    /// </summary>
    public abstract class RestAPIController : ControllerBase
    {
        /// <summary>
        /// Handle result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult HandleResult(IBaseResponse result)
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
    }
}

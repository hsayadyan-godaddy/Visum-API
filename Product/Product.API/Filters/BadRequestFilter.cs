using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Product.API.Models.Error;

namespace Product.API.Filters
{
    internal class BadRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result == null && !context.ModelState.IsValid)
            {
                var sb = new System.Text.StringBuilder();

                sb.Append("Bad Request");

                foreach (var modItem in context.ModelState)
                {
                    var hasMsg = false;
                    foreach (var err in modItem.Value.Errors)
                    {
                        if (!string.IsNullOrEmpty(err.ErrorMessage))
                        {
                            hasMsg = true;
                            sb.Append($". '{modItem.Key}' - {err.ErrorMessage}");
                        }
                    }

                    if (!hasMsg)
                    {
                        /* sb.Append($". '{modItem.Key}' - Is invalid"); */
                    }

                }


                var msg = new ServerError(StatusCodes.Status400BadRequest, sb.ToString());
                context.Result = new ObjectResult(msg)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

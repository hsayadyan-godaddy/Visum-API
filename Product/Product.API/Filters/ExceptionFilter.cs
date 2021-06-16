using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Product.API.Models.Error;
using System;
using System.Net;

namespace Product.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        #region members

        private readonly ILogger _logger;
        private HttpStatusCode _statusCode = HttpStatusCode.OK;

        #endregion

        #region ctor

        public ExceptionFilter(
            ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            _logger = loggerFactory.CreateLogger(GetType());
        }

        #endregion

        #region publics

        public void OnException(ExceptionContext context)
        {
            PrepareResponseForException(context.Exception);
            context.ExceptionHandled = true;

            var msg = new ServerError((int)_statusCode, context.Exception.Message);

            context.Result = new ObjectResult(msg)
            {
                StatusCode = (int)_statusCode
            };
        }

        #endregion

        #region privates

        private void PrepareResponseForException(Exception exception)
        {
            _statusCode = HttpStatusCode.InternalServerError;
            _logger.LogError(exception, nameof(ExceptionFilter));
        }

        #endregion
    }
}

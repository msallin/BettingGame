using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BettingGame.Framework.Web.ExceptionHandling
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception.GetType() == typeof(ValidationException))
            {
                var exception = (ValidationException)context.Exception;

                IList<string> messages = new List<string>(2);
                messages.Add(exception.ValidationResult.ErrorMessage);
                if (exception.ValidationResult.MemberNames.Any())
                {
                    string fields = string.Join(";", exception.ValidationResult.MemberNames);
                    messages.Add(fields);
                }

                var json = new JsonErrorResponse { Messages = messages.ToArray() };
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "Unknown error." }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}

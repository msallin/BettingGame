using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BettingGame.Framework.Web.ExceptionHandling
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            string[] validationErrors = context.ModelState.Keys.SelectMany(k => context.ModelState[k].Errors).Select(e => e.ErrorMessage).ToArray();
            var json = new JsonErrorResponse { Messages = validationErrors };

            context.Result = new BadRequestObjectResult(json);
        }
    }
}

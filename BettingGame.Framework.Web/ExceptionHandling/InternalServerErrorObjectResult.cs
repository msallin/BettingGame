using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BettingGame.Framework.Web.ExceptionHandling
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace BettingGame.Framework.Web.ExceptionHandling
{
    public static class ExceptionHandlingExtensions
    {
        public static MvcOptions AddExceptionHandling(this MvcOptions options)
        {
            options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            options.Filters.Add(typeof(ValidateModelStateFilter));
            return options;
        }
    }
}

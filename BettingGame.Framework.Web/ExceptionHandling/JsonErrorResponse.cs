namespace BettingGame.Framework.Web.ExceptionHandling
{
    public class JsonErrorResponse
    {
        public object DeveloperMessage { get; set; }

        public string[] Messages { get; set; }
    }
}

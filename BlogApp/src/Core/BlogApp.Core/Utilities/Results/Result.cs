namespace BlogApp.Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message, int httpStatusCode) : this(success, httpStatusCode)
        {
            Message = message;
            HttpStatusCode = httpStatusCode;
        }

        public Result(bool success, int httpStatusCode)
        {
            Success = success;
            HttpStatusCode = httpStatusCode;
        }

        public bool Success { get; }

        public string Message { get; }

        public int HttpStatusCode { get; }
    }
}


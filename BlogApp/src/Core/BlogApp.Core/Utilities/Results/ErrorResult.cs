namespace BlogApp.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message, int httpStatusCode) : base(false, message, httpStatusCode)
        {
        }

        public ErrorResult(int httpStatusCode) : base(false, httpStatusCode)
        {
        }
    }
}


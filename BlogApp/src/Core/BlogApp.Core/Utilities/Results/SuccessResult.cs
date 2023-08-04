namespace BlogApp.Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message, int httpStatusCode) : base(true, message, httpStatusCode)
        {
        }

        public SuccessResult(int httpStatusCode) : base(true, httpStatusCode)
        {
        }
    }
}


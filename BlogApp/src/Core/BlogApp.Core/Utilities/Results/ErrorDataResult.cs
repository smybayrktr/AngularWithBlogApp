namespace BlogApp.Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message, int httpStatusCode) : base(data, false, message, httpStatusCode)
        {
        }

        public ErrorDataResult(T data, int httpStatusCode) : base(data, false, httpStatusCode)
        {
        }

        public ErrorDataResult(string message, int httpStatusCode) : base(default, false, message, httpStatusCode)
        {

        }

        public ErrorDataResult(int httpStatusCode) : base(default, false, httpStatusCode)
        {

        }
    }
}


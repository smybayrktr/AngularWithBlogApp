namespace BlogApp.Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message, int httpStatusCode) : base(data, true, message, httpStatusCode)
        {
        }

        public SuccessDataResult(T data, int httpStatusCode) : base(data, true, httpStatusCode)
        {
        }

        public SuccessDataResult(string message, int httpStatusCode) : base(default, true, message, httpStatusCode)
        {

        }

       
    }
}


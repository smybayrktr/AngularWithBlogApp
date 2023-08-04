namespace BlogApp.Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message, int httpStatusCode) : base(success, message, httpStatusCode)
        {
            Data = data;
        }

        public DataResult(T data, bool success, int httpStatusCode) : base(success, httpStatusCode)
        {
            Data = data;
        }

        public T Data { get; }
    }
}


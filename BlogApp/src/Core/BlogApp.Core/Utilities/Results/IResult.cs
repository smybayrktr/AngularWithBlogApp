namespace BlogApp.Core.Utilities.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        public int HttpStatusCode { get; }
    }
}


namespace BlogApp.Core.Utilities.Helpers.UrlHelper
{
    public interface IUrlHelper
    {
        string ToSeoUrl(string title);
        public string AddBaseUrlToUrl(string url);
    }
}


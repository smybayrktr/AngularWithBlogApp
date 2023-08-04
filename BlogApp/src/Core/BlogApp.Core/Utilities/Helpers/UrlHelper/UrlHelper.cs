using Microsoft.Extensions.Configuration;

namespace BlogApp.Core.Utilities.Helpers.UrlHelper
{
    public class UrlHelper : IUrlHelper
    {
        private string _baseUrl = "";

        public UrlHelper(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("BaseUrl").Value;
        }

        public string AddBaseUrlToUrl(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                if (!url.Contains(_baseUrl))
                {
                    url = _baseUrl + url;
                }
            }

            return url;
        }

        public string ToSeoUrl(string title)
        {
            string seoTitle = title.Trim();
            seoTitle = seoTitle.ToLower();
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü', ' ' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U', '-' };
            for (int i = 0; i < turkishChars.Length; i++)
                seoTitle = seoTitle.Replace(turkishChars[i], englishChars[i]);
            seoTitle = seoTitle.Replace(" ", "-");
            seoTitle = seoTitle.Trim();
            seoTitle = seoTitle.Trim('-');
            return seoTitle;
        }
    }
}


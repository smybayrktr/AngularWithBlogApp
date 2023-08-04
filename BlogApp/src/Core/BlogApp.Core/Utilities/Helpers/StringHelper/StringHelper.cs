using System.Text.RegularExpressions;

namespace BlogApp.Core.Utilities.Helpers.StringHelper
{
    public static class StringHelper
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string RemoveHtml(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "";
            }
            String result = Regex.Replace(text, @"<[^>]*>", String.Empty);
            if (String.IsNullOrEmpty(result))
            {
                return "";
            }
            if (result.Length >= 30)
            {
                result = result.Substring(0, 10) + "...";
            }
            return result;
        }
    }
}


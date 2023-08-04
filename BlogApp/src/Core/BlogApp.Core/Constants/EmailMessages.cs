namespace BlogApp.Core.Constants;

public static class EmailMessages
{
    public const string RegisterTitle = "You are successfuly registered Süms Blog App";
    public const string RegisterSubject = "You are successfuly registered Süms Blog App";

    public static string GetRegisterBody(string name)
    {
        return $"<p>Hello {name}</p><br><p>Welcome to the Süms Blog App</p>";
    }

    public static string GetRegisterBodyWithPassword(string name, string password)
    {
        return $"<p>Hello {name}</p><br><p>Welcome to the Süms Blog App</p><br><p>Your password to login is {password}</p>";
    }
}
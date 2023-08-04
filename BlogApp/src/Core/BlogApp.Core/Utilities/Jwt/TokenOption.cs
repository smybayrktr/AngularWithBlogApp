namespace BlogApp.Core.Utilities.Jwt;

public class TokenOption
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double AccessTokenExpirationInMinutes { get; set; }
    public string SecurityKey { get; set; }
}
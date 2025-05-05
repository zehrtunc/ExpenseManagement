namespace ExpenseManagement.UI.Services;

public class AuthCookieService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthCookieService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void SetToken(string token, bool rememberMe)
    {
        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = rememberMe
                ? DateTimeOffset.UtcNow.AddDays(7)
                : DateTimeOffset.UtcNow.AddHours(1)
        };

        _contextAccessor.HttpContext?.Response.Cookies.Append("AuthToken", token, options);
    }

    public void DeleteToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete("AuthToken");
    }

    public string? GetToken()
    {
        string? token = null;
        _contextAccessor.HttpContext?.Request.Cookies.TryGetValue("AuthToken", out token);
        return token;
    }
}

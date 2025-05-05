using ExpenseManagement.UI.Services.ExpenseManagement.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseManagement.UI.Controllers;

public class BaseController : Controller
{
    protected readonly ApiRequestService _api;

    public BaseController(ApiRequestService api)
    {
        _api = api;
    }

    protected string? UserEmail => GetClaim(ClaimTypes.Email);
    protected long? UserId => long.TryParse(GetClaim(ClaimTypes.NameIdentifier), out var id) ? id : null;
    protected List<string> UserRoles => GetClaims(ClaimTypes.Role);
    protected bool IsInRole(string role) => UserRoles.Contains(role);

    private List<string> GetClaims(string type)
    {
        var token = Request.Cookies["AuthToken"];
        if (string.IsNullOrEmpty(token)) return new();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return jwt.Claims.Where(c => c.Type == type).Select(c => c.Value).ToList();
    }

    private string? GetClaim(string type)
    {
        var token = Request.Cookies["AuthToken"];
        if (string.IsNullOrEmpty(token)) return null;

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        return jwt.Claims.FirstOrDefault(c => c.Type == type)?.Value;
    }
}

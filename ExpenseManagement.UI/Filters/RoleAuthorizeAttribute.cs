using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExpenseManagement.UI.Filters;

public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string[] _roles;

    public RoleAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var token = context.HttpContext.Request.Cookies["AuthToken"];
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
            return;
        }

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userRoles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

        bool authorized = _roles.Any(r => userRoles.Contains(r));
        if (!authorized)
        {
            context.Result = new ForbidResult(); // 403
        }
    }
}

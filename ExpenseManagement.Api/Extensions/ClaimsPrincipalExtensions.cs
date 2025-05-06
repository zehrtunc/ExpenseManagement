using System.Security.Claims;

namespace ExpenseManagement.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? long.Parse(userIdClaim.Value) : throw new UnauthorizedAccessException("User ID not found");
        }
    }
}

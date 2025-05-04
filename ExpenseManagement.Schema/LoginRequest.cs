
namespace ExpenseManagement.Schema;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? RememberMe { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}


using ExpenseManagement.Schema;
using ExpenseManagement.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserManager _userManager;

    public AuthController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var success = await _userManager.RegisterAsync(request.Email, request.Password, request.Name, request.Surname);
        if (!success) return BadRequest(new ApiResponse("Bir hata oluştu"));
        return Ok(new ApiResponse());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _userManager.LoginAsync(request.Email, request.Password);
        if (token == null) return Unauthorized("Geçersiz bilgiler");

        var response = new LoginResponse { Token = token };
        return Ok(new ApiResponse<LoginResponse>(response));
    }
}

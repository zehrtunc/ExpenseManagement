
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Services.IServices;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure.Services;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasherService _passwordHasher;
    private readonly JwtTokenService _jwtTokenService;

    public UserManager(IUserRepository userRepository,
                       PasswordHasherService passwordHasher,
                       JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<bool> RegisterAsync(string email, string password, string name, string surname)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
            return false;

        var hashedPassword = _passwordHasher.HashPassword(password);

        var user = new User
        {
            Email = email,
            Name = name,
            Surname = surname,
            PasswordHash = hashedPassword
        };

        await _userRepository.AddAsync(user);
        return true;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return null;

        var isValid = _passwordHasher.VerifyPassword(password, user.PasswordHash);
        if (!isValid) return null;

        var roles = user.Roles?.Select(r => r.Name).ToArray() ?? Array.Empty<string>();
        var token = _jwtTokenService.GenerateToken(user.Id, user.Email, roles);
        return token;
    }

    public Task LogoutAsync()
    {
        // Eğer token bazlı logout yapılacaksa, blacklistleme yapılmalı
        return Task.CompletedTask;
    }
}


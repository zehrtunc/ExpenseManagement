
namespace ExpenseManagement.Services.IServices;

public interface IUserManager
{
    Task<bool> RegisterAsync(string email, string password, string name, string surname);
    Task<string?> LoginAsync(string email, string password);
    Task LogoutAsync(); // Token-based logout kullanılacaksa (opsiyonel)
}

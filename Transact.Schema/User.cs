using ExpenseManagement.Base;

namespace ExpenseManagement.Schema;

public class UserRequest : BaseRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

}

public class UserResponse : BaseResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

}

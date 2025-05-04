using ExpenseManagement.Base;


namespace ExpenseManagement.Schema;

public class RoleRequest : BaseRequest
{
    public string Name { get; set; }
}

public class RoleResponse : BaseResponse
{
    public string Name { get; set; }
}

using ExpenseManagement.Base;

namespace ExpenseManagement.Schema;

public class CustomerPhoneRequest : BaseRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class CustomerPhoneResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

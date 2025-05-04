using ExpenseManagement.Base;

namespace ExpenseManagement.Schema;

public class ExpenseCategoryRequest : BaseRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class ExpenseCategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
}

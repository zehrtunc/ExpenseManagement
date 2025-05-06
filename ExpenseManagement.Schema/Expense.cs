using ExpenseManagement.Base;
using ExpenseManagement.Schema.Enums;

namespace ExpenseManagement.Schema;

public class ExpenseRequest : BaseRequest
{
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public DateTime RequestDate { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? Location { get; set; } 
    public long? ReviewById { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? RejectionReason { get; set; }
}

public class ExpenseResponse : BaseResponse
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime RequestDate { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? Location { get; set; }
    public string? ReviewByUserName { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? RejectionReason { get; set; }

    public virtual List<ExpenseDocumentResponse> ExpenseDocuments { get; set; }
}

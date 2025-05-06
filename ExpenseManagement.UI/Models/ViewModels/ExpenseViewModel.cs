using ExpenseManagement.Schema;
using ExpenseManagement.Schema.Enums;

namespace ExpenseManagement.UI.Models.ViewModels;

public class ExpenseViewModel
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

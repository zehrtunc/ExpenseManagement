using ExpenseManagement.Base;
using ExpenseManagement.Base.Entities;

namespace ExpenseManagement.Schema;

public class PaymentTransactionRequest : BaseRequest
{
    public long ExpenseId { get; set; }
    public long BankAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string ReferencaNumber { get; set; }
}

public class PaymentTransactionResponse : BaseResponse
{
    public long ExpenseId { get; set; }
    public long BankAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string ReferencaNumber { get; set; }
}


using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class PaymentTransaction : BaseEntity
{
    public long ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }

    public long BankAccountId { get; set; }
    public virtual BankAccount BankAccount { get; set; }

    public decimal Amount { get; set; }
    public DateTime? TransactionDate { get; set; }
    public string ReferenceNumber { get; set; }
}

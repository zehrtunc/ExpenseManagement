

using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class BankAccount : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public string BankName { get; set; }
    public string CurrencyCode { get; set; }

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }
}


using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class ExpenseDocument : BaseEntity
{
    public long ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
}

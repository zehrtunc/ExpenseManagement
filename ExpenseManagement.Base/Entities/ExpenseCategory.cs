
using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class ExpenseCategory : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; }

}

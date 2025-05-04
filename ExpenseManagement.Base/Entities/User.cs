using System.Reflection.Metadata.Ecma335;
using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public virtual BankAccount BankAccount { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<Expense> ExpenseRequests { get; set; }
    public virtual ICollection<Expense> ExpenseReviews { get; set; }
}

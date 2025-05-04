
using ExpenseManagement.Base.Domain;

namespace ExpenseManagement.Base.Entities;

public  class Role : BaseEntity
{
    public string Name { get; set; }

    public virtual ICollection<User> Users{ get; set; }
}

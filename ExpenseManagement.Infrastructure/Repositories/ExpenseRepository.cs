
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(ApplicationDbContext context) : base(context)
    {
    }
}

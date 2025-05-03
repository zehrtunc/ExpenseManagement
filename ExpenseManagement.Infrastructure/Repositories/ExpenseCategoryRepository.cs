
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class ExpenseCategoryRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
{
    public ExpenseCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}

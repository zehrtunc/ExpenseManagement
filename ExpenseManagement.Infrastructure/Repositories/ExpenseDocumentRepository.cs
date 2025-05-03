
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class ExpenseDocumentRepository : BaseRepository<ExpenseDocument>, IExpenseDocumentRepository
{
    public ExpenseDocumentRepository(ApplicationDbContext context) : base(context)
    {
    }
}

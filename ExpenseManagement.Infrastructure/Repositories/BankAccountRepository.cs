
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
{
    public BankAccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}


using ExpenseManagement.Base.Entities;
using ExpenseManagement.Services;
using Transact.Api.Data;

namespace ExpenseManagement.Infrastructure;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}

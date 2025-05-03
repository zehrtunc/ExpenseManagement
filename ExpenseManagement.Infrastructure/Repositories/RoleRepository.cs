
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Infrastructure.Data;
using ExpenseManagement.Services;

namespace ExpenseManagement.Infrastructure;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}

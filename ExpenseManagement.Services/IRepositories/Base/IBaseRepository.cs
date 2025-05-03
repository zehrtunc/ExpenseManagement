using ExpenseManagement.Base.Domain;
using System.Linq.Expressions;

namespace ExpenseManagement.Services.IRepositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(long id, bool includePassive = false);
    Task<TEntity?> FindAsync(long id, bool includePassive = false);
    Task<List<TEntity>> GetAllAsync(bool includePassive = false);
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, bool includePassive = false);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool includePassive = false);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity, bool hardDelete = false);
    Task DeleteByIdAsync(long id, bool hardDelete = false);
    IQueryable<TEntity> Query(bool includePassive = false);
}

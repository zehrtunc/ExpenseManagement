

using Transact.Base.Domain;

namespace ExpenseManagement.Services;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> GetByIdAsync(long id);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task DeleteAsync(TEntity entity);
    public Task DeleteByIdAsync(long id);
}

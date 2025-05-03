using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ExpenseManagement.Infrastructure.Data; // ApplicationDbContext
using ExpenseManagement.Base.Entities;
using ExpenseManagement.Base.Domain;
using ExpenseManagement.Services.IRepositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<TEntity?> FindAsync(long id, bool includePassive = false)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
            return null;

        if (!includePassive && !entity.IsActive)
            return null;

        return entity;
    }
    public async Task<TEntity?> GetByIdAsync(long id, bool includePassive = false)
    {
        return await _dbSet
            .Where(x => x.Id == id && (includePassive || x.IsActive))
            .FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(bool includePassive = false)
    {
        return await _dbSet
            .Where(x => includePassive || x.IsActive)
            .ToListAsync();
    }

    public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, bool includePassive = false)
    {
        return await _dbSet
            .Where(predicate)
            .Where(x => includePassive || x.IsActive)
            .ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool includePassive = false)
    {
        return await _dbSet
            .Where(predicate)
            .Where(x => includePassive || x.IsActive)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null
            ? await _dbSet.CountAsync()
            : await _dbSet.CountAsync(predicate);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.IsActive = true;
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, bool hardDelete = false)
    {
        if (hardDelete)
        {
            _dbSet.Remove(entity);
        }
        else
        {
            entity.IsActive = false;
            entity.UpdateDate = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(long id, bool hardDelete = false)
    {
        var entity = await GetByIdAsync(id, includePassive: true);
        if (entity is not null)
            await DeleteAsync(entity, hardDelete);
    }

    public IQueryable<TEntity> Query(bool includePassive = false)
    {
        return _dbSet.AsQueryable().Where(x => includePassive || x.IsActive);
    }


}

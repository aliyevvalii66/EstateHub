using EstateHub.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly EstateHubDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(EstateHubDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }



    public async Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true)
    {
        if (asNoTracking)
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync(bool asNoTracking = true)
        => await ApplyTracking(_dbSet, asNoTracking).ToListAsync();

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        => await ApplyTracking(_dbSet, asNoTracking).Where(predicate).ToListAsync();

    public async Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
        => await ApplyTracking(_dbSet, asNoTracking).FirstOrDefaultAsync(predicate);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.CountAsync(predicate);

    public IQueryable<T> GetQueryable(bool asNoTracking = true)
        => ApplyTracking(_dbSet, asNoTracking);

    // Write
    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await _dbSet.AddRangeAsync(entities);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Delete(T entity)
        => _dbSet.Remove(entity);

    public void DeleteRange(IEnumerable<T> entities)
        => _dbSet.RemoveRange(entities);




    private IQueryable<T> ApplyTracking(IQueryable<T> query, bool asNoTracking)
    => asNoTracking ? query.AsNoTracking() : query;
}
using System.Linq.Expressions;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking = true);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true);
    Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = true);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> GetQueryable(bool asNoTracking = true); 
}
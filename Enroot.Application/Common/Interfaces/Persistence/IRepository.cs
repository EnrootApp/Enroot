using System.Linq.Expressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Application.Common.Interfaces.Persistence;
public interface IRepository<TAggregateRoot, TId>
where TAggregateRoot : AggregateRoot<TId>
where TId : notnull
{
    public Task<TAggregateRoot?> GetByIdAsync(TId id);
    public Task<TAggregateRoot?> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate);
    public Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot);
    public Task<TAggregateRoot> DeleteAsync(TAggregateRoot aggregateRoot);
    public Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot);
    public Task<IEnumerable<TAggregateRoot>> GetAllAsync();
    IQueryable<TAggregateRoot> Filter(Expression<Func<TAggregateRoot, bool>> predicate);
}
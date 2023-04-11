using System.Linq.Expressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Application.Common.Interfaces.Persistence;
public interface IRepository<TAggregateRoot, TId>
where TAggregateRoot : AggregateRoot<TId>
where TId : notnull
{
    Task<TAggregateRoot?> GetByIdAsync(
       TId id,
       bool includeDeleted = false,
       CancellationToken cancellationToken = default);
    Task<TAggregateRoot?> FindAsync(
       Expression<Func<TAggregateRoot, bool>> predicate,
       bool includeDeleted = false,
       CancellationToken cancellationToken = default);

    IQueryable<TAggregateRoot> Filter(
        Expression<Func<TAggregateRoot, bool>> predicate,
        bool includeDeleted = false);

    IQueryable<TAggregateRoot> GetAll(bool includeDeleted = false);

    Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken);
    Task<TAggregateRoot> DeleteAsync(TAggregateRoot aggregateRoot);
    Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot);
}
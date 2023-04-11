using System.Linq.Expressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Application.Common.Interfaces.Persistence;
public interface IReadRepository<TReadEntity>
where TReadEntity : ReadEntity
{
    public Task<TReadEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool includeDeleted = false, params Expression<Func<TReadEntity, object>>[] includeProps);
    public Task<TReadEntity?> FindAsync(Expression<Func<TReadEntity, bool>> predicate, CancellationToken cancellationToken, bool includeDeleted = false, params Expression<Func<TReadEntity, object>>[] includeProps);
    public IQueryable<TReadEntity> GetAll(bool includeDeleted = false);
    IQueryable<TReadEntity> Filter(Expression<Func<TReadEntity, bool>> predicate, bool includeDeleted = false);
}
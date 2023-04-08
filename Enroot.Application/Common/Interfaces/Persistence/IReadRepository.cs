using System.Linq.Expressions;
using Enroot.Domain.Common.Models;

namespace Enroot.Application.Common.Interfaces.Persistence;
public interface IReadRepository<TReadEntity>
where TReadEntity : ReadEntity
{
    public Task<TReadEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<TReadEntity, object>>[] includeProps);
    public Task<TReadEntity?> FindAsync(Expression<Func<TReadEntity, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<TReadEntity, object>>[] includeProps);
    public IQueryable<TReadEntity> GetAll();
    IQueryable<TReadEntity> Filter(Expression<Func<TReadEntity, bool>> predicate);
}
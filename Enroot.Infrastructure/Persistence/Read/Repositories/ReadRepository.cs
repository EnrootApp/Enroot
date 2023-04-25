using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence.Read.Repositories;

public class ReadRepository<TReadEntity> : IReadRepository<TReadEntity>
where TReadEntity : ReadEntity<Guid>
{
    private readonly EnrootReadonlyContext _context;

    public ReadRepository(EnrootReadonlyContext context)
    {
        _context = context;
    }

    public async Task<TReadEntity?> FindAsync(
        Expression<Func<TReadEntity, bool>> predicate,
        CancellationToken cancellationToken,
        bool includeDeleted = false,
        params Expression<Func<TReadEntity, object>>[] includeProps)
    {
        var entities = Filter(predicate, includeDeleted);

        foreach (var includeProp in includeProps)
        {
            entities = entities.Include(includeProp);
        }

        return await entities.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public IQueryable<TReadEntity> GetAll(bool includeDeleted = false)
    {
        var entities = _context.Set<TReadEntity>().AsQueryable();

        if (!includeDeleted)
        {
            entities = entities.Where(e => !e.IsDeleted);
        }

        return entities;
    }

    public IQueryable<TReadEntity> Filter(Expression<Func<TReadEntity, bool>> predicate, bool includeDeleted = false)
    {
        return GetAll(includeDeleted).Where(predicate);
    }

    public async Task<TReadEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken,
        bool includeDeleted = false,
        params Expression<Func<TReadEntity, object>>[] includeProps)
    {
        return await FindAsync(ag => ag.Id == id, cancellationToken, includeDeleted, includeProps);
    }
}
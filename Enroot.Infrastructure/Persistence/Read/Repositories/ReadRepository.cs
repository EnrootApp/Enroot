using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence.Read.Repositories;

public class ReadRepository<TReadEntity> : IReadRepository<TReadEntity>
where TReadEntity : ReadEntity
{
    private readonly EnrootReadonlyContext _context;

    public ReadRepository(EnrootReadonlyContext context)
    {
        _context = context;
    }

    public async Task<TReadEntity?> FindAsync(Expression<Func<TReadEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<TReadEntity>().AsQueryable().FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public IQueryable<TReadEntity> GetAll()
    {
        return _context.Set<TReadEntity>();
    }

    public IQueryable<TReadEntity> Filter(Expression<Func<TReadEntity, bool>> predicate)
    {
        return _context.Set<TReadEntity>().AsQueryable().Where(predicate);
    }

    public async Task<TReadEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await FindAsync(ag => ag.Id == id, cancellationToken);
    }
}
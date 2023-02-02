using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Models;
using Enroot.Infrastructure.Persistence.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence.Repositories;

public class Repository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
where TAggregateRoot : AggregateRoot<TId>
where TId : ValueObject
{
    private readonly EnrootContext _context;
    private readonly IMediator _mediator;

    public Repository(EnrootContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot)
    {
        var result = await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot);
        await SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TAggregateRoot> DeleteAsync(TAggregateRoot aggregateRoot)
    {
        var result = _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        await SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TAggregateRoot?> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate)
    {
        return await _context.Set<TAggregateRoot>().AsQueryable().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TAggregateRoot>> GetAllAsync()
    {
        return await _context.Set<TAggregateRoot>().ToListAsync();
    }

    public IQueryable<TAggregateRoot> Filter(Expression<Func<TAggregateRoot, bool>> predicate)
    {
        return _context.Set<TAggregateRoot>().AsQueryable().Where(predicate);
    }

    public async Task<TAggregateRoot?> GetByIdAsync(TId id)
    {
        return await FindAsync(ag => ag.Id == id);
    }

    public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot)
    {
        var result = _context.Set<TAggregateRoot>().Update(aggregateRoot);
        await SaveChangesAsync();

        return result.Entity;
    }

    private async Task SaveChangesAsync()
    {
        await _mediator.DispatchDomainEventsAsync<TId>(_context);
        await _context.SaveChangesAsync();
    }
}
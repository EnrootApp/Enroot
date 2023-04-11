using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Models;
using Enroot.Infrastructure.Persistence.Write.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence.Write.Repositories;

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

    public async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        var result = await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot, cancellationToken: cancellationToken);
        await SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TAggregateRoot> DeleteAsync(TAggregateRoot aggregateRoot)
    {
        var result = _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        await SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TAggregateRoot?> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate, bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        return await GetAll(includeDeleted).FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);
    }

    public IQueryable<TAggregateRoot> GetAll(bool includeDeleted = false)
    {
        var entities = _context.Set<TAggregateRoot>().AsQueryable();

        if (!includeDeleted)
        {
            entities = entities.Where(e => !e.IsDeleted);
        }

        return entities;
    }

    public IQueryable<TAggregateRoot> Filter(Expression<Func<TAggregateRoot, bool>> predicate, bool includeDeleted = false)
    {
        return GetAll(includeDeleted).Where(predicate);
    }

    public async Task<TAggregateRoot?> GetByIdAsync(TId id, bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        return await FindAsync(ag => ag.Id == id, includeDeleted, cancellationToken: cancellationToken);
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
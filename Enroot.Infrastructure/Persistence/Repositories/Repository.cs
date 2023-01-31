using System.Linq.Expressions;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Infrastructure.Persistence.Repositories;

public class Repository<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
where TAggregateRoot : AggregateRoot<TId>
where TId : ValueObject
{
    private readonly EnrootContext _context;

    public Repository(EnrootContext context)
    {
        _context = context;
    }

    public async Task<TAggregateRoot> CreateAsync(TAggregateRoot aggregateRoot)
    {
        var result = await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TAggregateRoot> DeleteAsync(TAggregateRoot aggregateRoot)
    {
        var result = _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        await _context.SaveChangesAsync();

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

    public async Task<TAggregateRoot?> GetByIdAsync(TId id)
    {
        return await FindAsync(ag => ag.Id == id);
    }

    public async Task<TAggregateRoot> UpdateAsync(TAggregateRoot aggregateRoot)
    {
        var result = _context.Set<TAggregateRoot>().Update(aggregateRoot);
        await _context.SaveChangesAsync();

        return result.Entity;
    }
}
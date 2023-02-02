using Enroot.Domain.Common.Interfaces;

namespace Enroot.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
where TId : notnull
{
    private List<IDomainEvent> _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot(TId id) : base(id)
    {
        _domainEvents = new List<IDomainEvent>();
    }

    protected AggregateRoot()
    {
        _domainEvents = new List<IDomainEvent>();
    }

    protected void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}
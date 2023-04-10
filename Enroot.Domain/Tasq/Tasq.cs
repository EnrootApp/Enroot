using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using ErrorOr;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.Tasq.ValueObjects.Statuses;
using Enroot.Domain.Tasq.Events;

namespace Enroot.Domain.Tasq;

public sealed class Tasq : AggregateRoot<TasqId>
{
    private readonly List<Assignment> _assignments = new();
    public IReadOnlyList<Assignment> Assignments => _assignments.AsReadOnly();

    public TenantId TenantId { get; private set; }
    public AccountId CreatorId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsCompleted => _assignments.Any(a => a.Status is DoneStatus);

    private Tasq() { }
    private Tasq(TasqId id, TenantId tenantId, AccountId creatorId, string? description, string title) : base(id)
    {
        TenantId = tenantId;
        CreatorId = creatorId;
        Description = description;
        Title = title;
    }

    public static ErrorOr<Tasq> Create(TenantId tenantId, AccountId creatorId, string? description, string title)
    {
        if (tenantId is null)
        {
            return Errors.Tenant.NotFound;
        }

        if (creatorId is null)
        {
            return Errors.Account.NotFound;
        }

        if (string.IsNullOrWhiteSpace(title) || title.Length > 255)
        {
            return Errors.Tasq.NotFound;
        }

        var tasq = new Tasq(TasqId.CreateUnique(), tenantId, creatorId, description, title);

        tasq.AddDomainEvent(new TasqCreatedDomainEvent(tenantId, tasq.Id));

        return tasq;
    }

    public ErrorOr<Tasq> AddAssignment(Assignment assignment)
    {
        if (assignment is null)
        {
            return Errors.Tasq.NotFound;
        }

        if (IsCompleted)
        {
            return Errors.Tasq.AlreadyCompleted;
        }

        if (_assignments.Any(a => a.Status is INotCompletedStatus))
        {
            return Errors.Tasq.AlreadyAssigned;
        }

        _assignments.Add(assignment);

        return this;
    }

    public ErrorOr<Tasq> UpdateDescription(string description, AccountId author)
    {
        Description = description;
        CreatorId = author;

        return this;
    }
}
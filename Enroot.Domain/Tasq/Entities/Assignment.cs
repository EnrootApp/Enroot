using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using Enroot.Domain.Tasq.ValueObjects.Statuses;

namespace Enroot.Domain.Tasq.Entities;

public sealed class Assignment : Entity<AssignmentId>
{
    private readonly List<Attachment> _attachments = new();

    public AccountId AssignerId { get; private set; }
    public AccountId AssigneeId { get; private set; }
    public StatusBase Status { get; private set; }
    public IReadOnlyList<Attachment> Attachments => _attachments.AsReadOnly();

    private Assignment() { }
    private Assignment(AccountId assignerId, AccountId assigneeId, StatusBase status)
    {
        AssignerId = assignerId;
        AssigneeId = assigneeId;
        Status = status;
    }

    public static ErrorOr<Assignment> Create(AccountId assignerId, AccountId assigneeId)
    {
        if (assignerId is null)
        {
            return Errors.Account.NotFound;
        }

        if (assigneeId is null)
        {
            return Errors.Account.NotFound;
        }

        return new Assignment(assigneeId, assigneeId, new ToDoStatus());
    }

    public ErrorOr<Assignment> CompleteStage()
    {
        var result = Status.Complete();

        if (result.IsError)
        {
            return result.FirstError;
        }

        Status = result.Value;
        return this;
    }

    public ErrorOr<Assignment> RejectStage()
    {
        var result = Status.Reject();

        if (result.IsError)
        {
            return result.FirstError;
        }

        Status = result.Value;
        return this;
    }
}
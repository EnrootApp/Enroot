using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using Enroot.Domain.Tasq.Enums;

namespace Enroot.Domain.Tasq.Entities;

public sealed class Assignment : Entity<AssignmentId>
{
    private readonly List<Attachment> _attachments = new();
    private readonly List<Status> _statuses = new();

    public AccountId AssignerId { get; private set; }
    public AccountId AssigneeId { get; private set; }

    public IReadOnlyList<Status> Statuses => _statuses.AsReadOnly();
    public IReadOnlyList<Attachment> Attachments => _attachments.AsReadOnly();
    public Status CurrentStatus => _statuses.OrderByDescending(s => s.CreatedOn).First();

    private Assignment() { }
    private Assignment(AssignmentId id, AccountId assignerId, AccountId assigneeId) : base(id)
    {
        AssignerId = assignerId;
        AssigneeId = assigneeId;
        _statuses.Add(Status.Create(StatusEnum.ToDo, assignerId, null).Value);
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

        return new Assignment(AssignmentId.CreateUnique(), assignerId, assigneeId);
    }

    public ErrorOr<Assignment> CompleteStage(AccountId approverId, string? feedbackMessage)
    {
        var result = CurrentStatus.Complete(approverId, feedbackMessage);

        if (result.IsError)
        {
            return result.FirstError;
        }

        _statuses.Add(result.Value);

        return this;
    }

    public ErrorOr<Assignment> RejectStage(AccountId approverId, string? feedbackMessage)
    {
        var result = CurrentStatus.Reject(approverId, feedbackMessage);

        if (result.IsError)
        {
            return result.FirstError;
        }

        _statuses.Add(result.Value);

        return this;
    }

    public ErrorOr<Assignment> AddAttachment(Attachment attachment)
    {
        _attachments.Add(attachment);
        return this;
    }
}
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Tasq.ValueObjects.Statuses;
using ErrorOr;

namespace Enroot.Domain.Tasq.Entities;

public sealed class Status : Entity<StatusBase>
{
    public FeedbackMessage? Feedback { get; private set; }
    public AccountId CreatorId { get; private set; }

    private Status() { }
    private Status(StatusBase status, FeedbackMessage feedback, AccountId creator) : base(status)
    {
        Feedback = feedback;
        CreatorId = creator;
    }
    public static ErrorOr<Status> Create(StatusEnum status, AccountId creator, string? feedbackMessage)
    {
        var feedback = FeedbackMessage.Create(feedbackMessage);

        if (feedback.IsError)
        {
            return feedback.Errors;
        }

        if (creator is null)
        {
            return Common.Errors.Errors.Account.NotFound;
        }

        return new Status(StatusBase.Create(status), feedback.Value, creator);
    }

    public ErrorOr<Status> Complete(AccountId creator, string feedbackMessage)
    {
        var feedback = FeedbackMessage.Create(feedbackMessage);

        if (feedback.IsError)
        {
            return feedback.Errors;
        }

        if (creator is null)
        {
            return Common.Errors.Errors.Account.NotFound;
        }

        var newStatus = Id.Complete();

        if (newStatus.IsError)
        {
            return newStatus.Errors;
        }

        return new Status(newStatus.Value, feedback.Value, creator);
    }

    public ErrorOr<Status> Reject(AccountId creator, string feedbackMessage)
    {
        var feedback = FeedbackMessage.Create(feedbackMessage);

        if (feedback.IsError)
        {
            return feedback.Errors;
        }

        if (creator is null)
        {
            return Common.Errors.Errors.Account.NotFound;
        }

        var newStatus = Id.Reject();

        if (newStatus.IsError)
        {
            return newStatus.Errors;
        }

        return new Status(newStatus.Value, feedback.Value, creator);
    }
}
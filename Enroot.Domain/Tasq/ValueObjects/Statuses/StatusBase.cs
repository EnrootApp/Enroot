using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public abstract class StatusBase : ValueObject
{
    public Status Value { get; protected set; }

    public abstract ErrorOr<StatusBase> Complete();
    public abstract ErrorOr<StatusBase> Reject();

    public static StatusBase Create(Status status)
    {
        return status switch
        {
            Status.ToDo => new ToDoStatus(),
            Status.InProgress => new InProgressStatus(),
            Status.AwaitingReview => new AwaitingReviewStatus(),
            Status.OnReview => new OnReviewStatus(),
            Status.Done => new DoneStatus(),
            Status.Rejected => new RejectedStatus(),
            Status.Cancelled => new CancelledStatus(),
            _ => new ToDoStatus()
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
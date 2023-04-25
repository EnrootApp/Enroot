using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public abstract class StatusBase : ValueObject
{
    public StatusEnum Value { get; protected set; }

    public abstract ErrorOr<StatusBase> Complete();
    public abstract ErrorOr<StatusBase> Reject();

    protected StatusBase(StatusEnum status)
    {
        Value = status;
    }

    public static StatusBase Create(StatusEnum status)
    {
        return status switch
        {
            StatusEnum.ToDo => new ToDoStatus(),
            StatusEnum.InProgress => new InProgressStatus(),
            StatusEnum.AwaitingReview => new AwaitingReviewStatus(),
            StatusEnum.Done => new DoneStatus(),
            StatusEnum.Rejected => new RejectedStatus(),
            StatusEnum.Cancelled => new CancelledStatus(),
            _ => new ToDoStatus()
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
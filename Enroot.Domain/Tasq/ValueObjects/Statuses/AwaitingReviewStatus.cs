using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class AwaitingReviewStatus : StatusBase, INotCompletedStatus
{
    public AwaitingReviewStatus()
    {
        Value = Status.AwaitingReview;
    }
    public override ErrorOr<StatusBase> Complete()
    {
        return new DoneStatus();
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return new RejectedStatus();
    }
}
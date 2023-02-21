using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class OnReviewStatus : StatusBase, INotCompletedStatus
{
    public OnReviewStatus()
    {
        Value = Status.OnReview;
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
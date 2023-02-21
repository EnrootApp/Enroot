using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class InProgressStatus : StatusBase, INotCompletedStatus
{
    public InProgressStatus()
    {
        Value = Status.InProgress;
    }
    public override ErrorOr<StatusBase> Complete()
    {
        return new AwaitingReviewStatus();
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return new CancelledStatus();
    }
}
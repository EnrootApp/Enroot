using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class RejectedStatus : StatusBase
{
    public RejectedStatus()
    {
        Value = Status.Rejected;
    }

    public override ErrorOr<StatusBase> Complete()
    {
        return Errors.Tasq.AlreadyCompleted;
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return Errors.Tasq.AlreadyCompleted;
    }
}
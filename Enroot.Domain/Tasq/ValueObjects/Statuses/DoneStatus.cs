using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class DoneStatus : StatusBase
{
    public DoneStatus()
    {
        Value = Status.Done;
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
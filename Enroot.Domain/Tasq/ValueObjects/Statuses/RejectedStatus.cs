using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using Enroot.Domain.Tasq.ValueObjects;
using Enroot.Domain.Account.ValueObjects;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class RejectedStatus : StatusBase
{
    public RejectedStatus() : base(StatusEnum.Rejected) { }

    public override ErrorOr<StatusBase> Complete()
    {
        return Errors.Tasq.AlreadyCompleted;
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return Errors.Tasq.AlreadyCompleted;
    }
}
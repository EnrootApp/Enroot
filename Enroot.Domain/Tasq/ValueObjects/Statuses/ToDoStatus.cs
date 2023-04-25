using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Enums;
using Enroot.Domain.Tasq.ValueObjects;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class ToDoStatus : StatusBase, INotCompletedStatus
{
    public ToDoStatus() : base(StatusEnum.ToDo) { }

    public override ErrorOr<StatusBase> Complete()
    {
        return new InProgressStatus();
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return new CancelledStatus();
    }
}
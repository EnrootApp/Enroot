using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public sealed class ToDoStatus : StatusBase, INotCompletedStatus
{
    public ToDoStatus()
    {
        Value = Status.ToDo;
    }

    public override ErrorOr<StatusBase> Complete()
    {
        return new InProgressStatus();
    }
    public override ErrorOr<StatusBase> Reject()
    {
        return new CancelledStatus();
    }
}
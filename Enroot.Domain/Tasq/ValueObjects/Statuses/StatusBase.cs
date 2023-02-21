using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects.Statuses;

public abstract class StatusBase : ValueObject
{
    public Status Value { get; protected set; }

    public abstract ErrorOr<StatusBase> Complete();
    public abstract ErrorOr<StatusBase> Reject();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
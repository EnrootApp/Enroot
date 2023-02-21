using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Tasq.ValueObjects;

public sealed class AssignmentId : ValueObject
{
    public Guid Value { get; }

    private AssignmentId(Guid value)
    {
        Value = value;
    }

    public static AssignmentId Create(Guid id) => new(id);
    public static AssignmentId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
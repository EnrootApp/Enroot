using Enroot.Domain.Common.Models;

namespace Enroot.Domain.Tasq.ValueObjects;

public sealed class TasqId : ValueObject
{
    public Guid Value { get; }

    private TasqId(Guid value)
    {
        Value = value;
    }

    public static TasqId Create(Guid id) => new(id);
    public static TasqId CreateUnique() => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
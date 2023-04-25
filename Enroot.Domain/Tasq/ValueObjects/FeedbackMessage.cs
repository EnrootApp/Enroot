using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects;

public sealed class FeedbackMessage : ValueObject
{
    public string? Value { get; }

    private FeedbackMessage(string? value)
    {
        Value = value;
    }

    public static ErrorOr<FeedbackMessage> Create(string? name)
    {
        if (name?.Length > 255)
        {
            return Errors.Tasq.FeedbackInvalid;
        }

        return new FeedbackMessage(name);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value ?? "";
    }
}
using ErrorOr;

namespace Enroot.Domain.Common.Errors;

public static partial class Errors
{
    // TODO: Localization
    public static class Leaf
    {
        public static Error RewardInvalid => Error.Validation(code: "Leaf.RewardInvalid", description: "Invalid reward value");
    }
}
using Enroot.Domain.Common.Errors;
using Enroot.Domain.Enums;
using ErrorOr;

namespace Enroot.Domain.Entities;

public class Leaf : Entity
{
    public ApproveType ApproveType { get; }
    public int Reward { get; }

    private Leaf() { }

    private Leaf(ApproveType approveType, int reward)
    {
        ApproveType = approveType;
        Reward = reward;
    }

    public static ErrorOr<Leaf> Create(ApproveType approveType, int reward)
    {
        if (reward <= 0)
        {
            return Errors.Leaf.RewardInvalid;
        }

        return new Leaf(approveType, reward);
    }
}
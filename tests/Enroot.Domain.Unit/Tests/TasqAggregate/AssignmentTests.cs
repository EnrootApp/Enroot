using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Entities;

namespace Enroot.Domain.Unit.Tests.TasqAggregate;

public class AssignmentTests
{
    [Fact]
    public void Complete_Should_ReturnAssignment()
    {
        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        var result = assignment.CompleteStage();
        Assert.False(result.IsError);
    }

    [Fact]
    public void Complete_Should_ReturnError()
    {
        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        assignment.CompleteStage(); // todo -> in progress
        assignment.CompleteStage(); // in progress -> awaiting review
        assignment.CompleteStage(); // awaiting review -> on review
        assignment.CompleteStage(); // on review -> done
        var result = assignment.CompleteStage(); // done -> error
        Assert.True(result.IsError);
    }

    [Fact]
    public void Reject_Should_ReturnError()
    {
        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        assignment.CompleteStage(); // todo -> in progress
        assignment.CompleteStage(); // in progress -> awaiting review
        assignment.RejectStage(); // awaiting review -> cancelled
        var result = assignment.RejectStage(); // cancelled -> error
        Assert.True(result.IsError);
    }
}
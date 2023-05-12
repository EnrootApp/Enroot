using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Entities;

namespace Enroot.Domain.Unit.Tests.TasqAggregate;

public class AssignmentTests
{
    [Fact]
    public void Complete_Should_ReturnAssignment()
    {
        var approverId = AccountId.CreateUnique();

        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        var result = assignment.CompleteStage(approverId, null);
        Assert.False(result.IsError);
    }

    [Fact]
    public void Complete_Should_ReturnError()
    {
        var approverId = AccountId.CreateUnique();

        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        assignment.CompleteStage(approverId, null); // todo -> in progress
        assignment.CompleteStage(approverId, null); // in progress -> awaiting review
        assignment.CompleteStage(approverId, null); // awaiting review -> on review
        assignment.CompleteStage(approverId, null); // on review -> done
        var result = assignment.CompleteStage(approverId, null); // done -> error
        Assert.True(result.IsError);
    }

    [Fact]
    public void Reject_Should_ReturnError()
    {
        var approverId = AccountId.CreateUnique();

        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;
        assignment.CompleteStage(approverId, null); // todo -> in progress
        assignment.CompleteStage(approverId, null); // in progress -> awaiting review
        assignment.RejectStage(approverId, "looser"); // awaiting review -> cancelled
        var result = assignment.RejectStage(approverId, "looser"); // cancelled -> error
        Assert.True(result.IsError);
    }
}
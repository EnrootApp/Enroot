using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tasq.Entities;
using Enroot.Domain.Tenant.ValueObjects;
using TasqEntity = Enroot.Domain.Tasq.Tasq;

namespace Enroot.Domain.Unit.Tests.TasqAggregate;

public class TasqTests
{
    [Fact]
    public void Create_Should_ReturnTenantNotFound()
    {
        var tasq = TasqEntity.Create(TenantId.CreateUnique(), AccountId.CreateUnique(), string.Empty, "Title");

        Assert.True(tasq.IsError && tasq.Errors.Contains(Domain.Common.Errors.Errors.Tenant.NotFound));
    }

    [Fact]
    public void Create_Should_ReturnAccountNotFound()
    {
        var tasq = TasqEntity.Create(TenantId.CreateUnique(), AccountId.CreateUnique(), string.Empty, "Title");

        Assert.True(tasq.IsError && tasq.Errors.Contains(Domain.Common.Errors.Errors.Account.NotFound));
    }

    [Fact]
    public void Create_Should_ReturnTasq()
    {
        var tasq = TasqEntity.Create(TenantId.CreateUnique(), AccountId.CreateUnique(), string.Empty, "Title");

        Assert.False(tasq.IsError);
    }

    [Fact]
    public void AddAssignment_Should_Add()
    {
        var tasq = TasqEntity.Create(TenantId.CreateUnique(), AccountId.CreateUnique(), string.Empty, "Title").Value;
        var assignment = Assignment.Create(AccountId.CreateUnique(), AccountId.CreateUnique()).Value;

        var result = tasq.AddAssignment(assignment);
        Assert.False(result.IsError);
    }

    [Fact]
    public void AddAssignment_Should_ReturnAlreadyAssignedError()
    {
        var assigneeGuid = Guid.NewGuid();

        var tasq = TasqEntity.Create(TenantId.CreateUnique(), AccountId.CreateUnique(), string.Empty, "Title").Value;
        var assignment1 = Assignment.Create(AccountId.CreateUnique(), AccountId.Create(assigneeGuid)).Value;
        var assignment2 = Assignment.Create(AccountId.CreateUnique(), AccountId.Create(assigneeGuid)).Value;

        tasq.AddAssignment(assignment1);
        var result = tasq.AddAssignment(assignment2);
        Assert.True(result.IsError && result.Errors.Contains(Domain.Common.Errors.Errors.Tasq.AlreadyAssigned));
    }
}
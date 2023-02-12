
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Domain.Unit.Tests.UserAggregate;

public class UserTests
{
    [Fact]
    public void AddAccountIdTest()
    {
        var user = User.User.CreateByEmail(Email.Create("email@mail.com"), "abc");

        Assert.False(user.IsError);

        var accountId = AccountId.CreateUnique();
        user.Value.AddAccountId(accountId);
        user.Value.AddAccountId(accountId);

        Assert.Distinct(user.Value.AccountIds);
    }
}
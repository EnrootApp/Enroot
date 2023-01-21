using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Account.ValueObjects;

namespace Enroot.Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; }
    public Username Username { get; }

    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    private User(UserId id, Email email, Username username) : base(id)
    {
        Email = email;
        Username = username;
    }

    public static User Create(Email email, Username username)
    {
        if (email is null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }

        return new User(UserId.CreateUnique(), email, username);
    }

    public void AddAccountId(AccountId id)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (_accountIds.Contains(id))
        {
            throw new ArgumentException();
        }

        _accountIds.Add(id);
    }
}
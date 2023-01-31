using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Account.ValueObjects;
using ErrorOr;

namespace Enroot.Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public Email? Email { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }

    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    private User() { }

    private User(UserId id, Email email, string passwordHash) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    private User(UserId id, PhoneNumber phoneNumber, string passwordHash) : base(id)
    {
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
    }

    public static ErrorOr<User> CreateByEmail(Email email, string passwordHash)
    {
        if (email is null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (passwordHash is null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        return new User(UserId.CreateUnique(), email, passwordHash);
    }

    public static ErrorOr<User> CreateByPhoneNumber(PhoneNumber phoneNumber, string passwordHash)
    {
        if (phoneNumber is null)
        {
            throw new ArgumentNullException(nameof(phoneNumber));
        }

        if (passwordHash is null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        return new User(UserId.CreateUnique(), phoneNumber, passwordHash);
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
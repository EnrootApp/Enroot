using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Errors;
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
            return Errors.User.EmailInvalid;
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Errors.User.EmailInvalid;
        }

        return new User(UserId.CreateUnique(), email, passwordHash);
    }

    public static ErrorOr<User> CreateByPhoneNumber(PhoneNumber phoneNumber, string passwordHash)
    {
        if (phoneNumber is null)
        {
            return Errors.User.PhoneInvalid;
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Errors.User.EmailInvalid;
        }

        return new User(UserId.CreateUnique(), phoneNumber, passwordHash);
    }

    public ErrorOr<User> AddAccountId(AccountId id)
    {
        if (id is null)
        {
            return Errors.Account.NotFoundById;
        }

        if (_accountIds.Contains(id))
        {
            return Errors.User.AccountExists;
        }

        _accountIds.Add(id);

        return this;
    }
}
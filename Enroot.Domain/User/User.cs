using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;
using Enroot.Domain.User.Enums;

namespace Enroot.Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; private set; }
    public string PasswordHash { get; set; }
    public string Role { get; private set; }
    public Name? FirstName { get; private set; }
    public Name? LastName { get; private set; }
    public string? AvatarUrl { get; private set; }

    private readonly List<AccountId> _accountIds = new();

    public IReadOnlyList<AccountId> AccountIds => _accountIds.AsReadOnly();

    private User() { }

    private User(UserId id, Email email, string passwordHash) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = UserRoles.Default;
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

    public ErrorOr<User> AddAccountId(AccountId id)
    {
        if (id is null)
        {
            return Errors.Account.NotFound;
        }

        if (_accountIds.Contains(id))
        {
            return Errors.Account.AlreadyExists;
        }

        _accountIds.Add(id);

        return this;
    }

    public ErrorOr<User> DeleteAccountId(AccountId id)
    {
        if (id is null)
        {
            return Errors.Account.NotFound;
        }

        if (!_accountIds.Contains(id))
        {
            return Errors.Account.NotFound;
        }

        _accountIds.Remove(id);

        return this;
    }

    public ErrorOr<User> UpdateInfo(Name firstName, Name lastName, string avatarUrl)
    {
        FirstName = firstName;
        LastName = lastName;
        AvatarUrl = avatarUrl;

        return this;
    }
}
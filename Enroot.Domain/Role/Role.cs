using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Permission.ValueObjects;

namespace Enroot.Domain.Role;

public sealed class Role : AggregateRoot<RoleId>
{
    private readonly List<PermissionId> _permissions = new();

    public string Name { get; }
    public IReadOnlyList<PermissionId> Permissions => _permissions.AsReadOnly();

    private Role(RoleId id, string name) : base(id)
    {
        Name = name;
    }

    public static Role Create(RoleId id, string name)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        return new(id, name);
    }
}

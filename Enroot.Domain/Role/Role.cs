using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Permission.Enums;
using ErrorOr;

namespace Enroot.Domain.Role;

public sealed class Role : AggregateRoot<RoleId>
{
    private readonly List<RolePermissionId> _permissions = new();

    public string Name { get; }
    public IReadOnlyList<RolePermissionId> Permissions => _permissions.AsReadOnly();

    private Role() { }

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

    public ErrorOr<Role> AddPermission(PermissionEnum permission)
    {
        var rolePermission = RolePermissionId.Create(permission);

        if (_permissions.Contains(rolePermission))
        {
            throw new ArgumentException($"'{nameof(permission)}' permission is already added.", nameof(permission));
        }

        _permissions.Add(rolePermission);

        return this;
    }
}

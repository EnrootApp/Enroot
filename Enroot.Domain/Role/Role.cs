using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Permission.Enums;
using ErrorOr;
using Enroot.Domain.Common.Errors;

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

    public static ErrorOr<Role> Create(RoleId id, string name)
    {
        if (id is null)
        {
            return Errors.Role.NotFoundById;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Errors.Role.InvalidName;
        }

        return new Role(id, name);
    }

    public ErrorOr<Role> AddPermission(PermissionEnum permission)
    {
        var rolePermission = RolePermissionId.Create(permission);

        if (_permissions.Contains(rolePermission))
        {
            return Errors.Role.PermissionExists;
        }

        _permissions.Add(rolePermission);

        return this;
    }
}

using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;
using Enroot.Domain.Permission.Enums;
using ErrorOr;
using Enroot.Domain.Common.Errors;
using Enroot.Domain.Permission.ValueObjects;

namespace Enroot.Domain.Role;

public sealed class Role : AggregateRoot<RoleId>
{
    private readonly List<PermissionId> _permissions = new();

    public string Name { get; }
    public IReadOnlyList<PermissionId> Permissions => _permissions.AsReadOnly();

    private Role() { }

    private Role(RoleId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static ErrorOr<Role> Create(RoleId id, string name)
    {
        if (id is null)
        {
            return Errors.Role.NotFound;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Errors.Role.InvalidName;
        }

        return new Role(id, name);
    }

    public ErrorOr<Role> AddPermission(PermissionId permission)
    {
        if (_permissions.Contains(permission))
        {
            return Errors.Role.PermissionExists;
        }

        _permissions.Add(permission);

        return this;
    }
}

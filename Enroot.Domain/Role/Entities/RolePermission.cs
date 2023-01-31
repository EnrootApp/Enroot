using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.ValueObjects;

namespace Enroot.Domain.Role.Entities;

public sealed class RolePermission : Entity<RolePermissionId>
{
    public string Description { get; }

    private RolePermission(RolePermissionId id, string description) : base(id)
    {
        Description = description;
    }

    public static RolePermission Create(RolePermissionId id, string description)
    {
        if (id is null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException($"'{nameof(description)}' cannot be null or whitespace.", nameof(description));
        }

        return new(id, description);
    }
}
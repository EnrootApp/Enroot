using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.ValueObjects;

namespace Enroot.Domain.Permission;

public sealed class Permission : AggregateRoot<PermissionId>
{
    public string Description { get; }

    private Permission() { }

    private Permission(PermissionId id, string description) : base(id)
    {
        Description = description;
    }

    public static Permission Create(PermissionId id, string description)
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
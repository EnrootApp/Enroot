using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.ValueObjects;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Permission;

public sealed class Permission : AggregateRoot<PermissionId>
{
    public string Description { get; }

    private Permission() { }

    private Permission(PermissionId id, string description) : base(id)
    {
        Description = description;
    }

    public static ErrorOr<Permission> Create(PermissionId id, string description)
    {
        if (id is null)
        {
            return Errors.Permission.NotFoundById;
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Errors.Permission.InvalidDescription;
        }

        return new Permission(id, description);
    }
}
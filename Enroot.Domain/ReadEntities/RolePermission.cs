using Enroot.Domain.Permission.Enums;
using Enroot.Domain.Role.Enums;

namespace Enroot.Domain.ReadEntities;

public class RolePermissionRead
{
    public RoleEnum RoleId { get; set; }
    public PermissionEnum PermissionId { get; set; }

    public virtual RoleRead Role { get; private set; } = default!;
    public virtual PermissionRead Permission { get; private set; } = default!;
}
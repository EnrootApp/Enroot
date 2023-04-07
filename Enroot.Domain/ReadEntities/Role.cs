using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.Enums;

namespace Enroot.Domain.ReadEntities;

public class RoleRead
{
    public RoleEnum Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual ICollection<RolePermissionRead> Permissions { get; private set; } = default!;
}
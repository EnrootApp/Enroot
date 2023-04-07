using Enroot.Domain.Common.Models;
using Enroot.Domain.Role.Enums;

namespace Enroot.Domain.ReadEntities;

public class AccountRead : ReadEntity
{
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public RoleEnum RoleId { get; private set; }
    public virtual UserRead User { get; private set; } = default!;
    public virtual RoleRead Role { get; private set; } = default!;
}
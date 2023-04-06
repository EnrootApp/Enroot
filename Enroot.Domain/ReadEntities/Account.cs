using Enroot.Domain.Common.Models;

namespace Enroot.Domain.ReadEntities;

public class AccountRead : ReadEntity
{
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public virtual UserRead User { get; private set; } = default!;
}
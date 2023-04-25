using Enroot.Domain.Common.Models;

namespace Enroot.Domain.ReadEntities;

public class TasqRead : ReadEntity<Guid>
{
    public string? Description { get; private set; }
    public string Title { get; private set; } = default!;
    public Guid TenantId { get; private set; }
    public Guid CreatorId { get; private set; }
    public AccountRead Creator { get; private set; } = default!;
    public virtual ICollection<AssignmentRead> Assignments { get; private set; } = default!;
}
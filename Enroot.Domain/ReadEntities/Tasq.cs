using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;

namespace Enroot.Domain.ReadEntities;

public class TasqRead : ReadEntity
{
    public string? Description { get; private set; }
    public string Title { get; private set; } = default!;
    public Guid TenantId { get; private set; }
    public Guid CreatorId { get; private set; }
    public AccountRead Creator { get; private set; } = default!;
    public virtual ICollection<AssignmentRead> Assignments { get; private set; } = default!;

    public AssignmentRead? CurrentAssignment => Assignments.OrderBy(a => a.Status).FirstOrDefault();
}
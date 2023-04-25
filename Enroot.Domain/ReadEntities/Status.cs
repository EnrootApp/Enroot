using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;

namespace Enroot.Domain.ReadEntities;

public class StatusRead : ReadEntity<StatusEnum>
{
    public string? Feedback { get; private set; }

    public Guid AssignmentId { get; private set; }
    public virtual AssignmentRead Assignment { get; private set; } = default!;

    public Guid CreatorId { get; private set; }
    public virtual AccountRead Creator { get; private set; } = default!;
}
using Enroot.Domain.Common.Models;
using Enroot.Domain.Tasq.Enums;

namespace Enroot.Domain.ReadEntities;

public class AssignmentRead : ReadEntity<Guid>
{
    public ICollection<StatusRead> Statuses { get; private set; } = default!;
    public ICollection<AttachmentRead> Attachments { get; private set; } = default!;
    public Guid TasqId { get; private set; }
    public Guid AssigneeId { get; private set; }
    public Guid AssignerId { get; private set; }

    public virtual TasqRead Tasq { get; private set; } = default!;
    public virtual AccountRead Assignee { get; private set; } = default!;
    public virtual AccountRead Assigner { get; private set; } = default!;
}
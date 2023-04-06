using Enroot.Domain.Common.Models;

namespace Enroot.Domain.ReadEntities;

public class AttachmentRead : ReadEntity
{
    public Guid AssignmentId { get; private set; }
    public virtual AssignmentRead Assignment { get; private set; } = default!;

    public string BlobUrl { get; private set; } = default!;
    public string Name { get; private set; } = default!;
}
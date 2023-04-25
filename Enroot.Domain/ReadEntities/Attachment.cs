using Enroot.Domain.Common.Models;

namespace Enroot.Domain.ReadEntities;

public class AttachmentRead : ReadEntity<Guid>
{
    public new int Id { get; private set; }
    public Guid AssignmentId { get; private set; }
    public virtual AssignmentRead Assignment { get; private set; } = default!;

    public string BlobUrl { get; private set; } = default!;
    public string Name { get; private set; } = default!;
}
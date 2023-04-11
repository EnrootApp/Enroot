namespace Enroot.Domain.Common.Models;

public abstract class ReadEntity
{
    public Guid Id { get; private set; }
    public int DbId { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public bool IsDeleted { get; private set; }
}
namespace Enroot.Domain.Common.Models;

public abstract class ReadEntity<T> where T : struct
{
    public T Id { get; private set; }
    public int DbId { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public bool IsDeleted { get; private set; }
}
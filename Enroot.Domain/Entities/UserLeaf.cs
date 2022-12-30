namespace Enroot.Domain.Entities;

public class UserLeaf : Entity
{
    //Attachment
    public bool UserLeafState { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public int LeafId { get; set; }
    public virtual Leaf Leaf { get; set; } = default!;
}
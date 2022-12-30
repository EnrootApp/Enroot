using Enroot.Domain.Enums;

namespace Enroot.Domain.Entities;

public class UserPreferences : Entity
{
    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public Locale Locale { get; set; } = Locale.EN;
    //public Location Location { get; set; } = default!;
}
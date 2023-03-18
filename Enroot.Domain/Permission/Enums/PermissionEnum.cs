using System.ComponentModel;

namespace Enroot.Domain.Permission.Enums;

public enum PermissionEnum
{
    [Description("Permission that allows task creating")]
    CreateTasq = 1,
    [Description("Permission that allows task reviewing")]
    ReviewTasq = 2,
    [Description("Permission that allows to create accounts")]
    CreateAccount = 3,
}
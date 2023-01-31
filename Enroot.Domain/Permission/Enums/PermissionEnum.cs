using System.ComponentModel;

namespace Enroot.Domain.Permission.Enums;

public enum PermissionEnum
{
    [Description("Permission that allows task creating")]
    CreateTask = 1,
    [Description("Permission that allows task reviewing")]
    ReviewTask = 2,
}
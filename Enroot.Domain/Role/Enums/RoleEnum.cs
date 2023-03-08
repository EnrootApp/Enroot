using System.ComponentModel;

namespace Enroot.Domain.Role.Enums;

public enum RoleEnum
{
    [Description("Admin of a tenant")]
    TenantAdmin = 1,
    [Description("Default tenant role")]
    Default = 2,
    [Description("Moderator role. Could review tasks")]
    Moderator = 3,
    [Description("Role of account, that was deactivated for the tenant")]
    Deactivated = 4,
}
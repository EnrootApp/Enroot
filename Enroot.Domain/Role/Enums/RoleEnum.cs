using System.ComponentModel;

namespace Enroot.Domain.Role.Enums;

public enum RoleEnum
{
    [Description("Admin of a tenant")]
    TenantAdmin = 1,
    [Description("Default tenant role")]
    Default = 2,
}
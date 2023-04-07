using Enroot.Domain.Common.Models;
using Enroot.Domain.Permission.Enums;

namespace Enroot.Domain.ReadEntities;

public class PermissionRead
{
    public PermissionEnum Id { get; set; }
    public DateTime CreatedOn { get; set; }
}
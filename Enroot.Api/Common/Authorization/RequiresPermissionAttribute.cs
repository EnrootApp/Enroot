using Enroot.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Enroot.Api.Common.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class RequiresPermissionAttribute : TypeFilterAttribute
{
    public RequiresPermissionAttribute(Permission permission) : base(typeof(RequiresPermissionFilter))
    {
        Arguments = new object[] {permission};
    }
}
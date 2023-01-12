using System.Security.Claims;
using Enroot.Domain.Common.Enums;
using Enroot.Domain.Entities;
using Enroot.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Api.Common.Authorization;

public class RequiresPermissionFilter : IAsyncAuthorizationFilter
{
    private readonly Permission _permission;

    public RequiresPermissionFilter(Permission permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User?.Identity is not ClaimsIdentity identity)
        {
            context.Result = new ForbidResult();
            return;
        }

        var userIdClaim = identity.Claims.FirstOrDefault(c => c.Type == JwtClaimNames.UserId);

        if (userIdClaim == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        int claimValue;

        try
        {
            claimValue = Convert.ToInt32(userIdClaim.Value);
        }
        catch
        {
            context.Result = new ForbidResult();
            return;
        }

        var userManager = context.HttpContext.RequestServices.GetService<UserManager<User>>();

        var user = await userManager!.Users
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == claimValue);

        if (user == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var permissions = user.Role.Claims;

        if (!permissions!.Any(permission => permission.Id == (int)_permission))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
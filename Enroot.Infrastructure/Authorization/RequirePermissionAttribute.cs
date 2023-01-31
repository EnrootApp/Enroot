using System.Net;
using Enroot.Application.Authentication.Queries.Login;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Enroot.Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Enroot.Infrastructure.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RequirePermissionAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly PermissionEnum _permission;

    public RequirePermissionAttribute(PermissionEnum permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var parsed = Guid.TryParse(context.HttpContext.Request.Headers["TenantId"].ToString(), out Guid tenantGuid);

        if (!parsed)
        {
            context.Result = new ForbidResult();
            return;
        }

        var tenantId = TenantId.Create(tenantGuid);

        var claim = context.HttpContext.User.Claims.First(c => c.Type == JwtClaimNames.UserId);
        var userId = UserId.Create(Guid.Parse(claim.Value));

        var accountRepository = context.HttpContext.RequestServices.GetService<IRepository<Account, AccountId>>();
        var account = await accountRepository!.FindAsync(a => a.TenantId == tenantId && a.UserId == userId);

        if (account is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var query = new HasPermissionQuery(account.Id, _permission);

        var mediator = context.HttpContext.RequestServices.GetService<ISender>();

        var result = await mediator!.Send(query);

        if (result.IsError || !result.Value)
        {
            context.Result = new ForbidResult();
            return;
        }

        return;
    }
}
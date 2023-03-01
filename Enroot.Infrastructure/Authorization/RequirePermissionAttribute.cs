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
        var accountIdClaim = context.HttpContext.User.FindFirst(EnrootClaimNames.AccountId);

        if (accountIdClaim is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var accountId = AccountId.Create(Guid.Parse(accountIdClaim.Value));

        var query = new HasPermissionQuery(accountId, _permission);

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
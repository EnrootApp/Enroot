using Enroot.Application.Authorization.HasPermission;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Permission.Enums;
using Enroot.Domain.Tenant.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Enroot.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class RequireTenantAccountAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var accountIdClaim = context.HttpContext.User.FindFirst(EnrootClaimNames.AccountId);

        if (accountIdClaim is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var tenantIdHeader = context.HttpContext.Request.Headers["TenantId"];

        if (string.IsNullOrWhiteSpace(tenantIdHeader))
        {
            context.Result = new ForbidResult();
            return;
        }

        var parsed = Guid.TryParse(tenantIdHeader, out Guid tenantIdGuid);

        if (!parsed)
        {
            context.Result = new ForbidResult();
            return;
        }

        var accountId = AccountId.Create(Guid.Parse(accountIdClaim.Value));
        var tenantId = TenantId.Create(tenantIdGuid);

        var repository = context.HttpContext.RequestServices.GetService<IRepository<Domain.Tenant.Tenant, TenantId>>();

        var tenant = await repository!.GetByIdAsync(tenantId, CancellationToken.None);

        if (tenant?.AccountIds.Contains(accountId) != true)
        {
            context.Result = new ForbidResult();
            return;
        }

        return;
    }
}
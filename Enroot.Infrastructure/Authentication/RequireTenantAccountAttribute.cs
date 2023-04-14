using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;

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

        var tenantIdClaim = context.HttpContext.User.FindFirst(EnrootClaimNames.TenantId);

        if (tenantIdClaim is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var tenantIdGuid = Guid.Parse(tenantIdClaim.Value);

        var tenantId = TenantId.Create(tenantIdGuid);
        var repository = context.HttpContext.RequestServices.GetService<IRepository<Domain.Tenant.Tenant, TenantId>>();
        var tenant = await repository!.GetByIdAsync(tenantId, cancellationToken: CancellationToken.None);

        var accountId = AccountId.Create(Guid.Parse(accountIdClaim.Value));

        if (tenant?.AccountIds.Contains(accountId) != true)
        {
            context.Result = new ForbidResult();
            return;
        }

        return;
    }
}
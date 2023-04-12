using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Enroot.Application.Account.Commands.Create;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Role.Enums;
using Enroot.Domain.Tenant.ValueObjects;
using MediatR;

namespace Enroot.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class RequireTenantAccountAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var isSystemAdmin = CheckIfUserIsSystemAdmin(context);

        var accountIdClaim = context.HttpContext.User.FindFirst(EnrootClaimNames.AccountId);

        if (accountIdClaim is null && !isSystemAdmin)
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

        if (accountIdClaim is null && isSystemAdmin)
        {
            var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userGuid = Guid.Parse(userIdClaim.Value);
            var createAccountCommand = new CreateAccountCommand(userGuid, tenantIdGuid, (int)RoleEnum.TenantAdmin);

            var account = await mediator!.Send(createAccountCommand);
            accountIdClaim = new Claim(EnrootClaimNames.AccountId, account.Value.Id.ToString());
        }

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

    private bool CheckIfUserIsSystemAdmin(AuthorizationFilterContext context)
    {
        var roleClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role);

        if (roleClaim is null)
        {
            return false;
        }

        return roleClaim.Value == "SystemAdmin";
    }
}
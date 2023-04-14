using System.Security.Claims;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Enroot.Infrastructure.Authentication;

public class ClaimsTransformer : IClaimsTransformation
{
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IRepository<Tenant, TenantId> _tenantRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsTransformer(
        IRepository<Account, AccountId> accountRepository,
        IHttpContextAccessor httpContextAccessor,
        IRepository<Tenant, TenantId> tenantRepository)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
        _tenantRepository = tenantRepository;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var currentPrincipal = (ClaimsIdentity)principal.Identity!;

        if (_httpContextAccessor.HttpContext is null)
        {
            return principal;
        }

        var tenantNameHeader = _httpContextAccessor.HttpContext.Request.Headers["Tenant"];

        var tenant = await _tenantRepository.FindAsync(t => t.Name.Value == tenantNameHeader.ToString());

        if (tenant is null)
        {
            return principal;
        }

        currentPrincipal.AddClaim(new Claim(EnrootClaimNames.TenantId, tenant.Id.Value.ToString()));

        var userIdClaimValue = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaimValue))
        {
            return principal;
        }

        var userId = UserId.Create(Guid.Parse(userIdClaimValue));

        var account = await _accountRepository.FindAsync(a => a.TenantId == tenant.Id && a.UserId == userId, cancellationToken: CancellationToken.None);

        if (account is null)
        {
            return principal;
        }

        currentPrincipal.AddClaim(new Claim(EnrootClaimNames.AccountId, account.Id.Value.ToString()));

        return principal;
    }
}
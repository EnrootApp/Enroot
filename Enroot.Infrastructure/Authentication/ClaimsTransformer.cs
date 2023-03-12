using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Domain.Account;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Tenant.ValueObjects;
using Enroot.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Enroot.Infrastructure.Authentication;

public class ClaimsTransformer : IClaimsTransformation
{
    private readonly IRepository<Account, AccountId> _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsTransformer(IRepository<Account, AccountId> accountRepository, IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return principal;
        }

        var tenantIdHeader = _httpContextAccessor.HttpContext.Request.Headers["TenantId"];

        var parsed = Guid.TryParse(tenantIdHeader, out Guid tenantGuid);

        if (!parsed)
        {
            return principal;
        }

        var tenantId = TenantId.Create(tenantGuid);

        var userIdClaimValue = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaimValue))
        {
            return principal;
        }

        var userId = UserId.Create(Guid.Parse(userIdClaimValue));

        var account = await _accountRepository.FindAsync(a => a.TenantId == tenantId && a.UserId == userId, CancellationToken.None);

        if (account is null)
        {
            return principal;
        }

        var currentPrincipal = (ClaimsIdentity)principal.Identity!;
        currentPrincipal.AddClaim(new Claim(EnrootClaimNames.AccountId, account.Id.Value.ToString()));

        return principal;
    }
}
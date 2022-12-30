using System.Security.Claims;

namespace Enroot.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, IList<Claim> claims);
}
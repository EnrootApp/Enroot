using System.Security.Claims;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserId userId);
}
using UserEntity = Enroot.Domain.User.User;

namespace Enroot.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserEntity user);
}
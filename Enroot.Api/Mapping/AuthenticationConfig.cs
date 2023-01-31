using Enroot.Application.Authentication.Commands.Register;
using Enroot.Application.Authentication.Common;
using Enroot.Application.Authentication.Queries.Login;
using Enroot.Contracts.Authentication;
using Mapster;

namespace Enroot.Api.Mapping;

public class AuthenticationConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>();
    }
}
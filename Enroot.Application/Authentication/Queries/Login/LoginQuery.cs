using Enroot.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
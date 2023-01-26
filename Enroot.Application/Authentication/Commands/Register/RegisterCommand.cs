using Enroot.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authentication.Commands.Register;

public record RegisterCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
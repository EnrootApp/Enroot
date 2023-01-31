using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authentication.Commands.Register;

public record CreateTenantCommand(string Name) : IRequest<ErrorOr<TenantResult>>;
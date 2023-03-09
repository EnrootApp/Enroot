using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Commands.Create;

public record CreateTenantCommand(string Name) : IRequest<ErrorOr<TenantResult>>;
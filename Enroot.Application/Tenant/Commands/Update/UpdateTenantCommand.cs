using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Commands.Update;

public record UpdateTenantCommand(Guid TenantId, string LogoUrl) : IRequest<ErrorOr<TenantResult>>;
using Enroot.Application.Tenant.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tenant.Commands.Delete;

public record DeleteTenantCommand(Guid TenantId) : IRequest<ErrorOr<TenantResult>>;
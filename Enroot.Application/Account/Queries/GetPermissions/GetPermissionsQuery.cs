using Enroot.Domain.Permission.Enums;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Queries.GetPermissions;

public record GetPermissionsQuery(Guid AccountId) : IRequest<ErrorOr<IEnumerable<PermissionEnum>>>;
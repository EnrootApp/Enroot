using Enroot.Domain.Common.Enums;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authorizatrion.Queries.Permissions
{
    public record CheckPermissionQuery(int UserId, RolePermissions Permission) : IRequest<ErrorOr<bool>>;
}

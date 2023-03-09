using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Permission.Enums;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authorization.HasPermission;

public record HasPermissionQuery(
    AccountId Id,
    PermissionEnum Permission
    ) : IRequest<ErrorOr<bool>>;
using Enroot.Domain.Account.ValueObjects;
using Enroot.Domain.Permission.Enums;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Authentication.Queries.Login;

public record HasPermissionQuery(
    AccountId Id,
    PermissionEnum Permission
    ) : IRequest<ErrorOr<bool>>;
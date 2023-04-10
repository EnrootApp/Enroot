using Enroot.Domain.Permission.Enums;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Account.Queries.GetMe;

public record GetMeQuery(Guid AccountId) : IRequest<ErrorOr<GetMeResult>>;
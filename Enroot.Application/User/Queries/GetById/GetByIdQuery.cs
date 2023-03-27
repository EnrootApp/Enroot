using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Queries.GetById;

public record GetByIdQuery(Guid Id) : IRequest<ErrorOr<UserResult>>;
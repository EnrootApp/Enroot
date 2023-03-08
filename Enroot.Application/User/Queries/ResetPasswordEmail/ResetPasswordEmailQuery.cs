using Enroot.Application.User.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.User.Queries.ResetPasswordEmail;

public record ResetPasswordEmailQuery(string Email) : IRequest<ErrorOr<UserResult>>;
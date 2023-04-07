using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Create;

public record CreateTasqCommand(Guid CreatorId, string? Description, string Title, Guid? AssigneeId) : IRequest<ErrorOr<TasqResult>>;
using Enroot.Application.Tasq.Common;
using ErrorOr;
using MediatR;

namespace Enroot.Application.Tasq.Commands.Update;

public record UpdateTasqCommand(Guid AuthorId, Guid TasqId, string Description) : IRequest<ErrorOr<TasqResult>>;

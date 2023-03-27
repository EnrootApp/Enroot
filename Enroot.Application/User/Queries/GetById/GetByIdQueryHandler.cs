using Enroot.Application.Common.Interfaces.Persistence;
using Enroot.Application.User.Common;
using Enroot.Domain.User.ValueObjects;

using UserEntity = Enroot.Domain.User.User;
using ErrorOr;
using Mapster;
using MediatR;

namespace Enroot.Application.User.Queries.GetById;


public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ErrorOr<UserResult>>
{
    private readonly IRepository<UserEntity, UserId> _userRepository;
    public GetByIdQueryHandler(IRepository<UserEntity, UserId> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResult>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.Id), cancellationToken);

        if (user is null)
        {
            return Domain.Common.Errors.Errors.User.NotFound;
        }

        return user.Adapt<UserResult>();
    }
}

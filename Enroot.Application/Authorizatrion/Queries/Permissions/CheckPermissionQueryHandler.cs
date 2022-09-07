using Enroot.Domain.Entities;
using ErrorOr;
using Enroot.Domain.Common.Errors;

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enroot.Application.Authorizatrion.Queries.Permissions
{
    public class CheckPermissionQueryHandler : IRequestHandler<CheckPermissionQuery, ErrorOr<bool>>
    {
        private readonly UserManager<User> _userManager;

        public CheckPermissionQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<bool>> Handle(CheckPermissionQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user == null)
            {
                return Errors.User.NotFoundById;
            }

            var permissions = user.Role.Claims;

            return permissions.FirstOrDefault(permission => permission.Id == (int)request.Permission) is not null;
        }
    }
}

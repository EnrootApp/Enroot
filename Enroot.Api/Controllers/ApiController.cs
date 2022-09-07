using Enroot.Api.Common.Http;
using Enroot.Infrastructure.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Enroot.Api.Controllers;

public class ApiController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected ApiController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected int? GetRequestUserId()
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity is not ClaimsIdentity identity)
        {
            return null;
        }

        var userIdClaim = identity.Claims.Where(c => c.Type == JwtClaimNames.UserId).FirstOrDefault();

        if (userIdClaim == null)
        {
            return null;
        }

        int claimValue;

        try
        {
            claimValue = Convert.ToInt32(userIdClaim.Value);
        }
        catch
        {
            return null;
        }

        return claimValue;
    }

    protected IActionResult Problem(IEnumerable<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}
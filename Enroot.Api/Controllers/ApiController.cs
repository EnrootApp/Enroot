using Enroot.Api.Common.Http;
using Enroot.Infrastructure.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace Enroot.Api.Controllers;

public class ApiController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer _localizer;

    protected ApiController(IHttpContextAccessor httpContextAccessor, IStringLocalizer localizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
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
        var localizedErrors = GetLocalizedErrors(errors);

        if (localizedErrors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in localizedErrors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails(modelStateDictionary);

            validationProblemDetails.Title = _localizer.GetString("Validation.General");

            return ValidationProblem(validationProblemDetails);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = localizedErrors;

        var firstError = localizedErrors.First();

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }

    private Error GetLocalizedError(Error error)
    {
        return Error.Custom(error.NumericType, error.Code, _localizer.GetString(error.Code));
    }

    private IEnumerable<Error> GetLocalizedErrors(IEnumerable<Error> errors)
    {
        return errors.ToList().ConvertAll(error => GetLocalizedError(error));
    }
}
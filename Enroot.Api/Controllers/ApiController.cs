using System.Reflection;
using Enroot.Api.Common.Http;
using Enroot.Infrastructure.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Enroot.Api.Controllers;

public abstract class ApiController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizerFactory _localizerFactory;

    protected ApiController(IHttpContextAccessor httpContextAccessor, IStringLocalizerFactory localizerFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizerFactory = localizerFactory;
    }

    protected Guid? GetRequestUserId()
    {
        return GetIdClaim(ClaimTypes.NameIdentifier);
    }

    protected Guid GetRequestAccountId()
    {
        return GetIdClaim(EnrootClaimNames.AccountId)!.Value;
    }

    protected Guid? GetIdClaim(string claim)
    {
        if (_httpContextAccessor.HttpContext?.User?.Identity is not ClaimsIdentity identity)
        {
            return null;
        }

        var userIdClaim = identity.Claims.FirstOrDefault(c => c.Type == claim);

        if (userIdClaim == null)
        {
            return null;
        }

        var parsed = Guid.TryParse(userIdClaim.Value, out Guid id);

        if (!parsed)
        {
            return null;
        }

        return id;
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

            var localizer = _localizerFactory.Create("Validation", Assembly.GetExecutingAssembly().FullName!);

            ValidationProblemDetails validationProblemDetails = new(modelStateDictionary)
            {
                Title = localizer.GetString("General")
            };

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

        return Problem(detail: firstError.Description, statusCode: statusCode, title: firstError.Code);
    }

    private Error GetLocalizedError(Error error)
    {
        var localizer = _localizerFactory.Create(error.Code, Assembly.GetExecutingAssembly().FullName!);

        return Error.Custom(error.NumericType, error.Code, localizer.GetString(error.Description));
    }

    private IEnumerable<Error> GetLocalizedErrors(IEnumerable<Error> errors)
    {
        return errors.ToList().ConvertAll(GetLocalizedError);
    }
}
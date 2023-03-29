using System.Reflection;
using Enroot.Api.Common.Http;
using Enroot.Infrastructure.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Enroot.Api.Common.Errors;

namespace Enroot.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizerFactory _localizerFactory;

    protected ApiController(
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizerFactory localizerFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizerFactory = localizerFactory;
        ProblemDetailsFactory = new EnrootProblemDetailsFactory();
    }

    protected Guid GetRequestUserId()
    {
        return GetIdClaim(ClaimTypes.NameIdentifier)!.Value;
    }

    protected Guid GetRequestAccountId()
    {
        return GetIdClaim(EnrootClaimNames.AccountId)!.Value;
    }

    protected Guid GetTenantId()
    {
        // TODO add tenantId filter;
        var tenantIdHeader = _httpContextAccessor.HttpContext!.Request.Headers["TenantId"];

        return Guid.Parse(tenantIdHeader!);
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
        var localizer = _localizerFactory.Create("Errors", Assembly.GetExecutingAssembly().FullName!);
        var title = localizer.GetString("General");
        var localizedErrors = GetLocalizedErrors(errors);

        if (localizedErrors.All(error => error.Type == ErrorType.Validation))
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in localizedErrors)
            {
                modelStateDictionary.AddModelError(
                    "Validation",
                    error.Description);
            }

            return ValidationProblem(ProblemDetailsFactory.CreateValidationProblemDetails(HttpContext, modelStateDictionary, title: title));
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

        return new ObjectResult(ProblemDetailsFactory.CreateProblemDetails(HttpContext, statusCode, title));
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
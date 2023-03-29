using Microsoft.AspNetCore.Mvc;

namespace Enroot.Api.Common.Errors;

public static class InvalidModelStateResponseFactory
{
    public static IActionResult GetBadRequestResult(ActionContext context)
    {
        var problemDetailsFactory = new EnrootProblemDetailsFactory();

        return new BadRequestObjectResult(problemDetailsFactory.CreateValidationProblemDetails(
            context.HttpContext,
            context.ModelState));
    }
}
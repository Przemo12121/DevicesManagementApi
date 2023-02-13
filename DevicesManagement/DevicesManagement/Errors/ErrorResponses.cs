using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Errors;

public static class ErrorResponses
{
    public static IActionResult CreateDetailed(int status, string? detail = null)
    {
        ProblemDetails details = new()
        {
            Detail = detail,
            Title = status switch
            {
                StatusCodes.Status404NotFound => StringMessages.HttpErrors.Titles.RESOURCE_NOT_FOUND,
                StatusCodes.Status401Unauthorized => StringMessages.HttpErrors.Titles.UNAUTHORIZED,
                StatusCodes.Status403Forbidden => StringMessages.HttpErrors.Titles.FORBIDDEN,
                StatusCodes.Status409Conflict => StringMessages.HttpErrors.Titles.CONFLICT,
                _ => null
            }
        };

        return status switch
        {
            StatusCodes.Status404NotFound => new NotFoundObjectResult(details),
            StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(details),
            StatusCodes.Status403Forbidden => new ObjectResult(details) { StatusCode = StatusCodes.Status403Forbidden },
            StatusCodes.Status409Conflict => new ConflictObjectResult(details),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }

    public static IActionResult CreatBadRequest(IDictionary<string, string[]> errors)
        => new BadRequestObjectResult(
            new ValidationProblemDetails(errors)
        );
}

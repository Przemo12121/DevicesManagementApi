using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Errors;

public static class ErrorResponses
{
    public static IActionResult CreateDetailed(int status, string? detail = null)
    {
        ProblemDetails details;
        switch (status)
        {
            case StatusCodes.Status400BadRequest:
                details = new() { Title = StringMessages.HttpErrors.Titles.BadRequest, Detail = detail };
                return new BadRequestObjectResult(details);
            case StatusCodes.Status401Unauthorized:
                details = new() { Title = StringMessages.HttpErrors.Titles.Unauthorized, Detail = detail };
                return new UnauthorizedObjectResult(details);
            case StatusCodes.Status403Forbidden:
                details = new() { Title = StringMessages.HttpErrors.Titles.Forbidden, Detail = detail };
                return new ObjectResult(details) { StatusCode = StatusCodes.Status403Forbidden };
            case StatusCodes.Status404NotFound:
                details = new() { Title = StringMessages.HttpErrors.Titles.ResourceNotFound, Detail = detail };
                return new NotFoundObjectResult(details);
            case StatusCodes.Status409Conflict:
                details = new() { Title = StringMessages.HttpErrors.Titles.Conflict, Detail = detail };
                return new ConflictObjectResult(details);
            default:
                details = new() { Title = StringMessages.HttpErrors.Titles.Internal, Detail = detail, Status = StatusCodes.Status500InternalServerError };
                return new ObjectResult(details) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    public static IActionResult CreatValidationFailures(IDictionary<string, string[]> errors)
        => new BadRequestObjectResult(
            new ValidationProblemDetails(errors)
        );
}

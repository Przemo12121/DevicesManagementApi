using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevicesManagement.Errors;

public static class ErrorResponses
{
    public static IActionResult Create(HttpStatusCode status, object? error)
    {
        ProblemDetails details;
        switch (status)
        {
            case HttpStatusCode.BadRequest:
                if (error is not IDictionary<string, string[]> errors) 
                    throw new Exception(StringMessages.InternalErrors.BAD_TYPE);

                details = new ValidationProblemDetails(errors);
                return new BadRequestObjectResult(details);
            case HttpStatusCode.NotFound:
                details = new ProblemDetails();
                return new NotFoundObjectResult(details);
            case HttpStatusCode.Unauthorized:
                details = new ProblemDetails();
                return new UnauthorizedObjectResult(details);
            case HttpStatusCode.Forbidden:
                details = new ProblemDetails();
                return new ObjectResult(details) { StatusCode = StatusCodes.Status403Forbidden };
            case HttpStatusCode.Conflict:
                details = new ProblemDetails();
                return new ConflictObjectResult(details);
            default:
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}

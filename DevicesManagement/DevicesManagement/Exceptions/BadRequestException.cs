using System.Net;

namespace DevicesManagement.Exceptions;

public class BadRequestException : Exception, IHttpException
{
    public IEnumerable<PropertyWithErrors> Failures { get; init; }
    public BadRequestException(IEnumerable<PropertyWithErrors> failures)
    {
        Failures = failures;
    }

    public async Task Execute(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsJsonAsync(Failures);
    }
}

public record PropertyWithErrors(string Property, IEnumerable<string> Errors);
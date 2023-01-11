using System.Net;

namespace DevicesManagement.Exceptions;

public class ForbiddenException : Exception, IHttpException
{
    public async Task Execute(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        await context.Response.WriteAsJsonAsync(new ReasonableException(StringMessages.HttpErrors.UNAUTHORIZED_TO_RESOURCE));
    }
}

public record ReasonableException(string Reason);
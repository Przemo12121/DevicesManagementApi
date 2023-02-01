using System.Net;

namespace DevicesManagement.Exceptions;

public class ForbiddenHttpException : Exception, IHttpException
{
    public async Task Execute(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        await context.Response.WriteAsJsonAsync(new ReasonableException(StringMessages.HttpErrors.UNAUTHORIZED_TO_RESOURCE));
    }
}
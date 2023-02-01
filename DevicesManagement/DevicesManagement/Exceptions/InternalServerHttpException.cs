using System.Net;

namespace DevicesManagement.Exceptions;

public class InternalServerHttpException : Exception, IHttpException
{
    public async Task Execute(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new ReasonableException(StringMessages.HttpErrors.INTERNAL));
    }
}

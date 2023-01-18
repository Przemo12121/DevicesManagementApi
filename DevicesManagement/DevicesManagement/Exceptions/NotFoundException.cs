using System.Net;

namespace DevicesManagement.Exceptions
{
    public class NotFoundException : Exception, IHttpException
    {
        public async Task Execute(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new ReasonableException(StringMessages.HttpErrors.RESOURCE_NOT_FOUND));
        }
    }
}

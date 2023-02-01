namespace DevicesManagement.Exceptions;

public class HttpExceptionHandler
{
    private readonly RequestDelegate _next;

    public HttpExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case IHttpException httpException:
                    await httpException.Execute(context);
                    break;
                default:
                    await new InternalServerHttpException().Execute(context);
                    break;
            }
        }
    }

}

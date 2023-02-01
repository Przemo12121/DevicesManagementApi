namespace DevicesManagement.Exceptions;

public interface IHttpException
{
    public Task Execute(HttpContext context);
}

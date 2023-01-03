using System.Net;
using System.Web.Http;

namespace Authentication.Results;

internal class AuthenticationFailureResult : IHttpActionResult
{
    public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
    {
        ReasonPhrase = reasonPhrase;
        Request = request;
    }

    public string ReasonPhrase { get; init; }

    public HttpRequestMessage Request { get; init; }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Unauthorized,
            RequestMessage = Request,
            ReasonPhrase = ReasonPhrase
        });
    }
}

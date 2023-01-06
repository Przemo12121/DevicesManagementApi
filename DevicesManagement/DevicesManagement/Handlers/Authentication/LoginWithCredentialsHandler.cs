using Authentication;
using Authentication.Jwt;
using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;
using MediatR;

namespace DevicesManagement.Handlers.Authentication;

public class LoginWithCredentialsHandler : IRequestHandler<LoginWithCredentialsRequest>
{
    private readonly IIdentityProvider<User> _identityProvider;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginWithCredentialsHandler(IHttpContextAccessor httpContextAccessor, IIdentityProvider<User> identityProvider, IJwtProvider jwtProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityProvider = identityProvider;
        _jwtProvider = jwtProvider;
    }

    public Task<Unit> Handle(LoginWithCredentialsRequest request, CancellationToken cancellationToken)
    {
        var user = _identityProvider.Identify(request.Login, request.Password);

        if (user == null)
        {
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            _httpContextAccessor.HttpContext.Response.Headers.WWWAuthenticate = "Invalid credentials";
        }
        else
        {
            var jwt = _jwtProvider.Generate(user);
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            _httpContextAccessor.HttpContext.Response.Headers.Authorization = jwt.RawData;
        }

        return Task.FromResult(new Unit());
    }
}

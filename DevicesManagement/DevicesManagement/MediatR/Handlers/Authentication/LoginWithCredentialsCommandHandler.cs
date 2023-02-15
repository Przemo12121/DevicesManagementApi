using Authentication.Jwt;
using DevicesManagement.MediatR.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.Handlers.Authentication;

public class LoginWithCredentialsCommandHandler : IRequestHandler<LoginWithCredentialsCommand, IActionResult>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginWithCredentialsCommandHandler(IHttpContextAccessor httpContextAccessor, IJwtProvider jwtProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtProvider = jwtProvider;
    }

    public Task<IActionResult> Handle(LoginWithCredentialsCommand request, CancellationToken cancellationToken)
    {
        var jwt = _jwtProvider.Generate(request.Identity);
        _httpContextAccessor.HttpContext!.Response.Headers.Authorization = jwt.RawData;

        return Task.FromResult<IActionResult>(new OkResult());
    }
}

using Authentication;
using DevicesManagement.MediatR.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Database.Models;

namespace DevicesManagement.MediatR.PipelineBehaviors;

public class CredentialsVerificationPipelineBehavior : IPipelineBehavior<LoginWithCredentialsCommand, IActionResult>
{
    private readonly IIdentityProvider<User> _identityProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CredentialsVerificationPipelineBehavior(IIdentityProvider<User> identityProvider, IHttpContextAccessor httpContextAccessor)
    {
        _identityProvider = identityProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Handle(LoginWithCredentialsCommand request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
    {
        var user = _identityProvider.Identify(request.Request.Login, request.Request.Password);
        if (user is null)
        {
            _httpContextAccessor.HttpContext!.Response.Headers.WWWAuthenticate = StringMessages.HttpErrors.INVALID_CREDENTIALS;
            return new UnauthorizedResult();
        }

        request.Identity = user;

        return await next();
    }
}

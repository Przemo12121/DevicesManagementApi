using Authentication;
using DevicesManagement.MediatR.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Database.Models;
using DevicesManagement.Errors;

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
        var user = await _identityProvider.Identify(request.Request.Login, request.Request.Password);
        if (user is null)
        {
            _httpContextAccessor.HttpContext!.Response.Headers.WWWAuthenticate = StringMessages.HttpErrors.Details.INVALID_CREDENTIALS;
            return ErrorResponses.CreateDetailed(
                StatusCodes.Status401Unauthorized, 
                StringMessages.HttpErrors.Details.INVALID_CREDENTIALS
            );
        }

        request.Identity = user;

        return await next();
    }
}

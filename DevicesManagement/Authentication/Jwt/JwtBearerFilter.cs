using Authentication.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web.Http.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Jwt;

public class JwtBearerFilter : System.Attribute, IAuthenticationFilter
{
    readonly JwtSecurityTokenHandler _handler;
    readonly TokenValidationParameters _options;

    public JwtBearerFilter(JwtSecurityTokenHandler handler, TokenValidationParameters options)
    {
        _handler = handler;
        _options = options;
    }

    public bool AllowMultiple { get; } = true;

    public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    {
        AuthenticationHeaderValue? authorization = context.Request.Headers?.Authorization;

        // if request is not meant for this filter, skip so the next filter can handle
        if (authorization == null || authorization.Scheme != JwtBearerDefaults.AuthenticationScheme) return;

        if (String.IsNullOrEmpty(authorization.Parameter))
        {
            context.ErrorResult = new AuthenticationFailureResult("Token missing", context.Request);
            return;
        }
        
        try
        {
            // verify jwt
            var claims = _handler.ValidateToken(authorization.Parameter, _options, out SecurityToken validatedToken);

            var employeeId = claims.FindFirst("employeeId")?.Value;
            var role = claims.FindFirst("role")?.Value;
            if (String.IsNullOrEmpty(employeeId) || String.IsNullOrEmpty(role))
            {
                context.ErrorResult = new AuthenticationFailureResult("Token invalid", context.Request);
                return;
            }

            // Jwt valid
            context.Principal = PrincipalFactory.CreateUserWithRole(employeeId, role);
        }
        catch (Exception)
        {
            // Jwt invalid
            context.ErrorResult = new AuthenticationFailureResult("Token invalid", context.Request);
        }
    }

    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
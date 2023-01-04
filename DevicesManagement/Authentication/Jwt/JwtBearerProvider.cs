using Database.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Jwt;

public class JwtBearerProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly SigningCredentials _signingCredentials;
    private JwtSecurityTokenHandler Handler { get; } = new JwtSecurityTokenHandler();
    public JwtBearerProvider(JwtOptions options)
    {
        _options = options;
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_options.Secret)
                        ),
            _options.Algorithm
            );
    }

    public JwtSecurityToken Generate(User user)
    {
        var descriptor = new SecurityTokenDescriptor()
        {
            Audience = _options.Audience,
            Issuer = _options.Issuer,
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMilliseconds(_options.ExpirationMs),
            SigningCredentials = _signingCredentials,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.EmployeeId),
                new Claim(ClaimTypes.Role, user.AccessLevel.Value.ToString())
            })
        };

        return (JwtSecurityToken)Handler.CreateToken(descriptor);
    }
}

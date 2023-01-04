using Database.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication.Jwt;

public interface IJwtProvider
{
    public JwtSecurityToken Generate(User user);
}

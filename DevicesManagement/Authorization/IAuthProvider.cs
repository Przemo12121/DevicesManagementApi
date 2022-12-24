using Database.Models.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Authorization;

/// <summary>
/// User's authentication provider.
/// </summary>
public interface IAuthProvider
{
    public IUser Authenticate(string login, string password);

    public IUser Authenticate(JwtSecurityToken token);
}

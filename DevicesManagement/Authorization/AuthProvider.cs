using Database.Models.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Authorization;

public class AuthProvider : IAuthProvider
{
    public IUser Authenticate(string login, string password)
    {
        throw new NotImplementedException();
    }

    public IUser Authenticate(JwtSecurityToken token)
    {
        throw new NotImplementedException();
    }
}

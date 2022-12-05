using DevicesMenagement.Database.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DevicesMenagement.Modules.Auth
{
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
}

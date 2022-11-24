using DevicesMenagement.Database.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DevicesMenagement.Modules.Auth
{
    /// <summary>
    /// User's authentication provider.
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Authenticates cretentials. On failed attempt throws exception.
        /// </summary>
        /// <param name="login">User's unique login value.</param>
        /// <param name="password">User's raw password.</param>
        /// <returns>Instance of user described by credentials</returns>
        public IUser Authenticate(string login, string password);

        /// <summary>
        /// Authenticates JWT. On failed attempt throws exception.
        /// </summary>
        /// <param name="token">The token to be authenticated.</param>
        /// <returns>Instance of user described by the token</returns>
        public IUser Authenticate(JwtSecurityToken token);
    }
}

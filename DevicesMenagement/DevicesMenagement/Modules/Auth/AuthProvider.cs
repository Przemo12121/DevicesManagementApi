using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.Auth
{
    public class AuthProvider : IAuthProvider
    {
        public IUser VerifyCredentials(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}

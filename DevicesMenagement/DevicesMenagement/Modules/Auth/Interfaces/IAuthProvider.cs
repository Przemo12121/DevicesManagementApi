using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.Auth
{
    public interface IAuthProvider
    {
        public IUser VerifyCredentials(string login, string password);
    }
}

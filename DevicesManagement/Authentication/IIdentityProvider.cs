using Database.Models;
using Database.Models.Interfaces;

namespace Authentication;

public interface IIdentityProvider<T> where T : class
{
    public T? Identify(string keyName, string password);
    public T CreateIdentity(string keyName, string name, string password, AccessLevel accessLevel);
    public void SaveIdentity(T user);
}

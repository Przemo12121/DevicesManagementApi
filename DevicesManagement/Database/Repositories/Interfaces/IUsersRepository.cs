using Database.Models;

namespace Database.Repositories.Interfaces;

public interface IUsersRepository : IDisposable, ITransactionableRepository, IResourceAuthorizableRepository<User>
{
    void Add(User user);
    void Delete(User user);
    void Update(User user);

    User? FindByEmployeeId(string eid);
    List<User> FindEmployees<TOrderKey>(ISearchOptions<User, TOrderKey> options);
    List<User> FindAdmins<TOrderKey>(ISearchOptions<User, TOrderKey> options);
}

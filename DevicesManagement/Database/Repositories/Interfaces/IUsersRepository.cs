using Database.Models;

namespace Database.Repositories.Interfaces;

public interface IUsersRepository : IDisposable, ITransactionableRepository, IResourceAuthorizableRepository<User>
{
    void Add(User user);
    void Delete(User user);
    void Update(User user);

    Task<User?> FindByEmployeeIdAsync(string eid);
    Task<List<User>> FindEmployeesAsync<TOrderKey>(ISearchOptions<User, TOrderKey> options);
    Task<List<User>> FindAdminsAsync<TOrderKey>(ISearchOptions<User, TOrderKey> options);

    Task<int> CountEmployeesAsync();
}

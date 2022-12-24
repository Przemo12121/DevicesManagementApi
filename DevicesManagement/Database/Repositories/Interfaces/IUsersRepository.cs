using Database.Models.Interfaces;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

public interface IUsersRepository<T> : IDisposable where T : IUser
{
    void Add(T user);
    void Delete(T user);
    void Update(T user);

    T? FindByEmployeeId(string eid);
    List<T> FindEmployees<TOrderKey>(ISearchOptions<T, TOrderKey> options);
    List<T> FindAdmins<TOrderKey>(ISearchOptions<T, TOrderKey> options);
}

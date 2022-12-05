using Database.Models.Interfaces;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

public interface ILocalAuthStorageRepository : IDisposable, IUpdatableRepository<IUser>
{
    IUser FindByEmployeeId(string eid);
    List<IUser> FindAllEmployees();
    List<IUser> FindAllEmployees(ISearchOptions<IUser> options);
    List<IUser> FindAllAdmins();
    List<IUser> FindAllAdmins(ISearchOptions<IUser> options);
}

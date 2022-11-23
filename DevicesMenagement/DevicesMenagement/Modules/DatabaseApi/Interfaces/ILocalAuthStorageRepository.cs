using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ILocalAuthStorageRepository : IDisposable
    {
        IUser FindByEmployeeId(string eid);
        List<IUser> FindAllEmployees();
        List<IUser> FindAllEmployees(ISearchOptions<IUser> options);
        List<IUser> FindAllAdmins();
        List<IUser> FindAllAdmins(ISearchOptions<IUser> options);
        void Add(ICreatableModelBuilder<IUser> builder);
        void Update(IUpdatableModelBuilder<IUser> builder);
        void Delete(IUser user);
    }
}

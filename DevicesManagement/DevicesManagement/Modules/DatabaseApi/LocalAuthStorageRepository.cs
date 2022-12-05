using DevicesMenagement.Database.Models;
using DevicesMenagement.Database;
using DevicesMenagement.Modules.DatabaseApi.InnerDependencies;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public class LocalAuthStorageRepository : DisposableRepository<LocalAuthStorageContext>, IDisposable, IUpdatableRepository<IUser>, ICreatableRepository<IUser>
    {
        public LocalAuthStorageRepository(LocalAuthStorageContext context) : base(context) { }

        public void Add(ICreatableModelBuilder<IUser> builder)
        {
            throw new NotImplementedException();
        }

        public void Delete(IUser entity)
        {
            throw new NotImplementedException();
        }

        public List<IUser> FindAllAdmins()
        {
            throw new NotImplementedException();
        }

        public List<IUser> FindAllAdmins(ISearchOptions<IUser> options)
        {
            throw new NotImplementedException();
        }

        public List<IUser> FindAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<IUser> FindAllEmployees(ISearchOptions<IUser> options)
        {
            throw new NotImplementedException();
        }

        public IUser FindByEmployeeId(string eid)
        {
            throw new NotImplementedException();
        }

        public void Update(IUpdatableModelBuilder<IUser> builder)
        {
            throw new NotImplementedException();
        }
    }
}

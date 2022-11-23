using Microsoft.EntityFrameworkCore;
using DevicesMenagement.Modules.DatabaseApi.InnerDependencies;
using DevicesMenagement.Database;
using DevicesMenagement.Modules.DatabaseApi.Builders;
using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public class DevicesRepository : DisposableRepository<DeviceMenagementContext>, IDeviceRepository
    {
        public DevicesRepository(DeviceMenagementContext context) : base(context) { }

        public void Add(ICreatableModelBuilder<IDevice> builder)
        {
            throw new NotImplementedException();
        }

        public void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> command)
        {
            throw new NotImplementedException();
        }

        public void AddHistory(IDevice device, ICreatableModelBuilder<IDeviceHistory> command)
        {
            throw new NotImplementedException();
        }

        public void Delete(IDevice entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(IDevice device)
        {
            throw new NotImplementedException();
        }

        public void DeleteHistory(IDevice device)
        {
            throw new NotImplementedException();
        }

        public List<IDevice> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<IDevice> FindAll(ISearchOptions<IDevice> options)
        {
            throw new NotImplementedException();
        }

        public List<IDevice> FindAllByEmployeeId(string employeeId)
        {
            throw new NotImplementedException();
        }

        public List<IDevice> FindAllByEmployeeId(string employeeId, ISearchOptions<IDevice> options)
        {
            throw new NotImplementedException();
        }

        public IDevice FindByIn(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(IUpdatableModelBuilder<IDevice> builder)
        {
            throw new NotImplementedException();
        }
    }
}

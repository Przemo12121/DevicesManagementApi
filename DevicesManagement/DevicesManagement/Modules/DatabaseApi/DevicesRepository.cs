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

        public void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> builder)
        {
            throw new NotImplementedException();
        }

        public void AddCommandHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder)
        {
            throw new NotImplementedException();
        }

        public void AddMessageHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder)
        {
            throw new NotImplementedException();
        }

        public void Delete(IDevice entity)
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

        public IDevice FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ICommandHistory> GetCommandHistory(int deviceId)
        {
            throw new NotImplementedException();
        }

        public List<ICommandHistory> GetCommandHistory(int deviceId, ISearchOptions<ICommandHistory> options)
        {
            throw new NotImplementedException();
        }

        public List<ICommand> GetCommands(int deviceId)
        {
            throw new NotImplementedException();
        }

        public List<ICommand> GetCommands(int deviceId, ISearchOptions<ICommand> options)
        {
            throw new NotImplementedException();
        }

        public List<IMessage> GetMessageHistory(int deviceId)
        {
            throw new NotImplementedException();
        }

        public List<IMessage> GetMessageHistory(int deviceId, ISearchOptions<ICommandHistory> options)
        {
            throw new NotImplementedException();
        }

        public void Update(IUpdatableModelBuilder<IDevice> builder)
        {
            throw new NotImplementedException();
        }
    }
}

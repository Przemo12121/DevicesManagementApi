using Database.Repositories.InnerDependencies;
using Database.Contexts;
using Database.Repositories.Builders;
using Database.Models;
using Database.Models.Interfaces;
using Database.Repositories.Interfaces;

namespace Database.Repositories;

public class DevicesRepository : DisposableRepository<DeviceManagementContext>, IDeviceRepository<Device>
{
    public DevicesRepository(DeviceManagementContext context) : base(context) { }

    public void Add(Device entity)
    {
        throw new NotImplementedException();
    }

    public void AddCommand(Device device, ICreatableModelBuilder<ICommand> builder)
    {
        throw new NotImplementedException();
    }

    public void AddCommandHistory(Device device, ICreatableModelBuilder<ICommandHistory> builder)
    {
        throw new NotImplementedException();
    }

    public void AddMessageHistory(Device device, ICreatableModelBuilder<ICommandHistory> builder)
    {
        throw new NotImplementedException();
    }

    public void Delete(Device entity)
    {
        throw new NotImplementedException();
    }

    public List<Device> FindAll()
    {
        throw new NotImplementedException();
    }

    public List<Device> FindAll<T>(ISearchOptions<Device, T> options)
    {
        throw new NotImplementedException();
    }

    public List<Device> FindAllByEmployeeId(string employeeId)
    {
        throw new NotImplementedException();
    }

    public List<Device> FindAllByEmployeeId<T>(string employeeId, ISearchOptions<Device, T> options)
    {
        throw new NotImplementedException();
    }

    public Device FindById(int id)
    {
        throw new NotImplementedException();
    }

    public List<ICommandHistory> GetCommandHistory(int deviceId)
    {
        throw new NotImplementedException();
    }

    public List<ICommandHistory> GetCommandHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options)
    {
        throw new NotImplementedException();
    }

    public List<ICommand> GetCommands(int deviceId)
    {
        throw new NotImplementedException();
    }

    public List<ICommand> GetCommands<U>(int deviceId, ISearchOptions<ICommand, U> options)
    {
        throw new NotImplementedException();
    }

    public List<IMessage> GetMessageHistory(int deviceId)
    {
        throw new NotImplementedException();
    }

    public List<IMessage> GetMessageHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options)
    {
        throw new NotImplementedException();
    }

    public void Update(Device entity)
    {
        throw new NotImplementedException();
    }
}

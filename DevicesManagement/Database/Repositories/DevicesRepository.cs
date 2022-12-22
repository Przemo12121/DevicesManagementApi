using Database.Repositories.InnerDependencies;
using Database.Contexts;
using Database.Repositories.Builders;
using Database.Models;
using Database.Models.Interfaces;
using Database.Repositories.Interfaces;
using Database.Models.Enums;

namespace Database.Repositories;

public class DevicesRepository : DisposableRepository<DeviceManagementContext>, IDeviceRepository<Device, Command, CommandHistory, Message>
{
    public DevicesRepository(DeviceManagementContext context) : base(context) { }

    public void Add(Device entity)
    {
        _context.Devices.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Device entity)
    {
        _context.Devices.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Device entity)
    {
        _context.Devices.Remove(entity);
        _context.SaveChanges();
    }

    public void AddCommand(Device device, Command command)
    {
        throw new NotImplementedException();
    }

    public void AddCommandHistory(Device device, CommandHistory commandHistory)
    {
        throw new NotImplementedException();
    }

    public void AddMessageToHistory(Device device, Message message)
    {
        throw new NotImplementedException();
    }

    public List<Device> FindAll<TOrderKey>(ISearchOptions<Device, TOrderKey> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Skip(options.Offset)
                .OrderBy(options.Order)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Skip(options.Offset)
                .OrderByDescending(options.Order)
                .Take(options.Limit)
                .ToList();
    }

    public List<Device> FindAllByEmployeeId<T>(string employeeId, ISearchOptions<Device, T> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Where(device => device.EmployeeId == employeeId)
                .Skip(options.Offset)
                .OrderBy(options.Order)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Where(device => device.EmployeeId == employeeId)
                .Skip(options.Offset)
                .OrderByDescending(options.Order)
                .Take(options.Limit)
                .ToList();
    }

    public Device? FindById(Guid id)
    {
        return _context.Devices
            .Where(device => device.Id == id)
            .SingleOrDefault();
    }

    public List<ICommandHistory> GetCommandHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options)
    {
        throw new NotImplementedException();
    }

    public List<ICommand> GetCommands<U>(int deviceId, ISearchOptions<ICommand, U> options)
    {
        throw new NotImplementedException();
    }

    public List<IMessage> GetMessageHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options)
    {
        throw new NotImplementedException();
    }
}

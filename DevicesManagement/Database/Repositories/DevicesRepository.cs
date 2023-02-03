using Database.Repositories.InnerDependencies;
using Database.Contexts;
using Database.Repositories.Builders;
using Database.Models;
using Database.Models.Interfaces;
using Database.Repositories.Interfaces;
using Database.Models.Enums;
using System.Linq;

namespace Database.Repositories;

public class DevicesRepository : DisposableRepository<DeviceManagementContext>, IDeviceRepository<Device, Command, CommandHistory, Message>, IResourceAuthorizableRepository<Device>
{
    public DevicesRepository(DeviceManagementContext context) : base(context) { }

    public void Add(Device entity)
        => _context.Devices.Add(entity);

    public void Update(Device entity)
        => _context.Devices.Update(entity);

    public void Delete(Device entity)
        => _context.Devices.Remove(entity);

    public void AddCommand(Device device, Command command)
    {
        _context.Commands.Add(command);
        device.Commands ??= new();
        device.Commands?.Add(command);
    }

    public void AddMessage(Device device, Message message)
    {
        _context.DevicesMessageHistory.Add(message);
        device.Messages.Add(message);
    }

    public List<Device> FindAll<TOrderKey>(ISearchOptions<Device, TOrderKey> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .OrderBy(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .OrderByDescending(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList();
    }

    public List<Device> FindAllByEmployeeId<T>(string employeeId, ISearchOptions<Device, T> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Where(device => device.EmployeeId.Equals(employeeId))
                .Skip(options.Offset)
                .OrderBy(options.Order)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Where(device => device.EmployeeId.Equals(employeeId))
                .Skip(options.Offset)
                .OrderByDescending(options.Order)
                .Take(options.Limit)
                .ToList();
    }

    public Device? FindById(Guid id)
    {
        return _context.Devices
            .Where(device => device.Id.Equals(id))
            .SingleOrDefault();
    }

    public List<CommandHistory> GetCommandHistories<U>(Guid deviceId, ISearchOptions<CommandHistory, U> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Commands)
                .SelectMany(command => command.CommandHistories)
                .OrderBy(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Commands)
                .SelectMany(command => command.CommandHistories)
                .OrderByDescending(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList();
    }

    public List<Command> GetCommands<U>(Guid deviceId, ISearchOptions<Command, U> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Commands)
                .OrderBy(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Commands)
                .OrderByDescending(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList();
    }

    public List<Message> GetMessages<U>(Guid deviceId, ISearchOptions<Message, U> options)
    {
        return options.OrderDirection == OrderDirections.ASCENDING
            ? _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Messages)
                .OrderBy(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList()
            : _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .SelectMany(device => device.Messages)
                .OrderByDescending(options.Order)
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToList();
    }

    public Device? FindByIdAndOwnerId(Guid id, string employeeId)
        => _context.Devices
            .Where(device => device.Id.Equals(id) && device.EmployeeId.Equals(employeeId))
            .SingleOrDefault();
}

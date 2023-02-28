using Database.Contexts;
using Database.Models;
using Database.Models.Enums;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Database.Repositories;

public class DevicesRepository : DisposableRepository<DevicesManagementContext>, IDevicesRepository, IResourceAuthorizableRepository<Device>
{
    public DevicesRepository(DevicesManagementContext context) : base(context) { }

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
        device.Messages ??= new();
        device.Messages.Add(message);
    }

    public Task<List<Device>> FindAllAsync<TOrderKey>(ISearchOptions<Device, TOrderKey> options)
    {
        var query = options.OrderDirection.Equals(OrderDirections.Ascending)
            ? _context.Devices.OrderBy(options.Order)
            : _context.Devices.OrderByDescending(options.Order);

        return query.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<List<Device>> FindAllByEmployeeIdAsync<T>(string employeeId, ISearchOptions<Device, T> options)
    {
        var query = _context.Devices
                .Where(device => device.EmployeeId.Equals(employeeId));

        var orderedQuery = options.OrderDirection == OrderDirections.Ascending
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery
                .Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<Device?> FindByIdAsync(Guid id)
        => _context.Devices
            .Where(device => device.Id.Equals(id))
            .FirstOrDefaultAsync();

    public Task<List<CommandHistory>> GetCommandHistoriesAsync<U>(Guid deviceId, ISearchOptions<CommandHistory, U> options)
    {
        var query = _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .Take(1)
                .SelectMany(device => device.Commands)
                .SelectMany(command => command.CommandHistories);

        var orderedQuery = options.OrderDirection == OrderDirections.Ascending
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<List<CommandHistory>> GetCommandHistories(Guid deviceId)
        => _context.Devices
            .SelectMany(device => device.Commands)
            .SelectMany(command => command.CommandHistories)
            .Where(device => device.Id.Equals(deviceId))
            .OrderBy(x => x.Id)
            .Skip(1)
            .Take(3)
            .ToListAsync();

    public Task<List<Command>> GetCommandsAsync<U>(Guid deviceId, ISearchOptions<Command, U> options)
    {
        var query = _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .Take(1)
                .SelectMany(device => device.Commands);

        var orderedQuery = options.OrderDirection == OrderDirections.Ascending
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<List<Message>> GetMessagesAsync<U>(Guid deviceId, ISearchOptions<Message, U> options)
    {
        var query = _context.Devices
                .Where(device => device.Id.Equals(deviceId))
                .Take(1)
                .SelectMany(device => device.Messages);

        var orderedQuery = options.OrderDirection == OrderDirections.Ascending
            ? query.OrderBy(options.Order)
            : query.OrderByDescending(options.Order);

        return orderedQuery.Skip(options.Offset)
                .Take(options.Limit)
                .ToListAsync();
    }

    public Task<Device?> FindByIdAndOwnerIdAsync(Guid id, string employeeId)
        => _context.Devices
            .Where(device => device.Id.Equals(id) && device.EmployeeId.Equals(employeeId))
            .FirstOrDefaultAsync();

    public Task<int> CountAsync(Expression<Func<Device, bool>> predicate)
        => _context.Devices
            .Where(predicate)
            .CountAsync();

    public Task<int> CountAsync()
       => _context.Devices.CountAsync();

    public Task<int> CountCommandsAsync(Guid deviceId) 
        => _context.Devices
            .Where(device => device.Id.Equals(deviceId))
            .Take(1)
            .SelectMany(device => device.Commands)
            .CountAsync();
}

using Database.Models;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class CommandsRepository : DisposableRepository<DevicesManagementContext>, ICommandsRepository
{
    public CommandsRepository(DevicesManagementContext context) : base(context) { }

    public void Delete(Command command)
        => _context.Commands.Remove(command);

    public void Update(Command entity)
        => _context.Commands.Update(entity);

    public void AddCommandHistory(Command command, CommandHistory commandHistory)
    {
        _context.DevicesCommandHistory.Add(commandHistory);
        command.CommandHistories.Add(commandHistory);
    }

    public Task<Command?> FindByIdAndOwnerIdAsync(Guid id, string employeeId)
        => _context.Devices
            .Where(device => device.EmployeeId.Equals(employeeId))
            .Take(1)
            .Include(device => device.Commands)
            .SelectMany(device => device.Commands)
            .Where(command => command.Id.Equals(id))
            .FirstOrDefaultAsync();

    public Task<Command?> FindByIdAsync(Guid id)
        => _context.Commands
            .Where(command => command.Id.Equals(id))
            .FirstOrDefaultAsync();
}

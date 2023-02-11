using Database.Models;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class CommandRepository : DisposableRepository<DevicesManagementContext>, ICommandsRepository
{
    public CommandRepository(DevicesManagementContext context) : base(context) { }

    public void Delete(Command command)
        => _context.Commands.Remove(command);

    public void Update(Command entity)
        => _context.Commands.Update(entity);

    public void AddCommandHistory(Command command, CommandHistory commandHistory)
    {
        _context.DevicesCommandHistory.Add(commandHistory);
        command.CommandHistories.Add(commandHistory);
    }

    public Command? FindByIdAndOwnerId(Guid id, string employeeId)
    {
        return _context.Devices
            .Where(device => device.EmployeeId.Equals(employeeId))
            .Include(device => device.Commands)
            .SelectMany(device => device.Commands)
            .Where(command => command.Id.Equals(id))
            .SingleOrDefault();
    }

    public Command? FindById(Guid id)
    {
        return _context.Commands
            .Where(command => command.Id.Equals(id))
            .SingleOrDefault();
    }
}

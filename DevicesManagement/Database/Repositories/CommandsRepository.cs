using Database.Models;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Contexts;

namespace Database.Repositories;

public class CommandsRepository : DisposableRepository<DeviceManagementContext>, ICommandsRepository<Command, CommandHistory>
{
    public CommandsRepository(DeviceManagementContext context) : base(context)
    {
    }

    public void Delete(Command command)
    {
        _context.Commands.Remove(command);
        _context.SaveChanges();
    }

    public void Update(Command entity)
    {
        _context.Commands.Update(entity);
        _context.SaveChanges();
    }
    public void AddCommandHistory(Command command, CommandHistory commandHistory)
    {
        _context.DevicesCommandHistory.Add(commandHistory);
        command.CommandHistories.Add(commandHistory);
        _context.SaveChanges();
    }
}

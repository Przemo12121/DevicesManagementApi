using Database.Models;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Contexts;

namespace Database.Repositories;

public class CommandsRepository : DisposableRepository<DeviceManagementContext>, ICommandsRepository<Command>
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
}

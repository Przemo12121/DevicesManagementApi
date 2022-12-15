using Database.Models.Interfaces;
using Database.Models;
using Database.Repositories.Builders;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;
using Database.Contexts;

namespace Database.Repositories;

public class CommandsRepository : DisposableRepository<DeviceManagementContext>, IUpdatableRepository<ICommand>
{
    public CommandsRepository(DeviceManagementContext context) : base(context)
    {
    }

    public void Delete(Command command)
    {
        _context.Commands.Remove(command);
        _context.SaveChanges();
    }

    public void Update(IUpdatableModelBuilder<ICommand> builder)
    {
        _context.Update(builder.Build());
        _context.SaveChanges();
    }
}

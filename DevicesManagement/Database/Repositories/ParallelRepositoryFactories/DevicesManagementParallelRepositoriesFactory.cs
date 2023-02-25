using Database.Contexts;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.ParallelRepositoryFactories;

public class DevicesManagementParallelRepositoriesFactory : IDevicesManagementParallelRepositoriesFactory
{
    /*private DbContextOptions<DevicesManagementContext> _options;
    public DevicesManagementParallelRepositoriesFactory(DbContextOptions<DevicesManagementContext> options)
    {
        _options = options;
    }*/

    public ICommandsRepository CreateCommandsRepository()
        => new CommandsRepository(new DevicesManagementContext());

    public IDevicesRepository CreateDevicesRepository()
        => new DevicesRepository(new DevicesManagementContext());
}

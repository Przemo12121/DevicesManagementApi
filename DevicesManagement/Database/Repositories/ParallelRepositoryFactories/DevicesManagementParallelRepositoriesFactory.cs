using Database.Contexts;
using Database.Repositories.Interfaces;

namespace Database.Repositories.ParallelRepositoryFactories;

public class DevicesManagementParallelRepositoriesFactory : IDevicesManagementParallelRepositoriesFactory
{
    public ICommandsRepository CreateCommandsRepository()
        => new CommandsRepository(new DevicesManagementContext());

    public IDevicesRepository CreateDevicesRepository()
        => new DevicesRepository(new DevicesManagementContext());
}

using Database.Repositories.Interfaces;

namespace Database.Repositories.ParallelRepositoryFactories;

public interface IDevicesManagementParallelRepositoriesFactory
{
    IDevicesRepository CreateDevicesRepository();
    ICommandsRepository CreateCommandsRepository();
}

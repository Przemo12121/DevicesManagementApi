using Database.Repositories.Interfaces;

namespace Database.Repositories.ParallelRepositoryFactories;

public interface ILocalAuthParallelRepositoriesFactory
{
    IUsersRepository CreateUsersRepository();
    IAccessLevelsRepository CreateAccessLevelsRepository();
}

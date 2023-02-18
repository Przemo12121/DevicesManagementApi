using Database.Contexts;
using Database.Repositories.Interfaces;

namespace Database.Repositories.ParallelRepositoryFactories;

public class LocalAuthParallelRepositoriesFactory : ILocalAuthParallelRepositoriesFactory
{
    public IUsersRepository CreateUsersRepository()
        => new UsersRepository(new LocalAuthStorageContext());
    public IAccessLevelsRepository CreateAccessLevelsRepository()
        => new AccessLevelsRepository(new LocalAuthStorageContext());
}

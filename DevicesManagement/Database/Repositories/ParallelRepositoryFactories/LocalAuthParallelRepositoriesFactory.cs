using Database.Contexts;
using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.ParallelRepositoryFactories;

public class LocalAuthParallelRepositoriesFactory : ILocalAuthParallelRepositoriesFactory
{
    private DbContextOptions<LocalAuthContext> _options;
    public LocalAuthParallelRepositoriesFactory(DbContextOptions<LocalAuthContext> options)
    {
        _options = options;
    }

    public IUsersRepository CreateUsersRepository()
        => new UsersRepository(new LocalAuthContext(_options));
    public IAccessLevelsRepository CreateAccessLevelsRepository()
        => new AccessLevelsRepository(new LocalAuthContext(_options));
}

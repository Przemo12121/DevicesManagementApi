using Database.Contexts;
using Database.Models;
using Database.Models.Enums;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;

namespace Database.Repositories;

public class AccessLevelsRepository : DisposableRepository<LocalAuthContext>, IAccessLevelsRepository
{
    public AccessLevelsRepository(LocalAuthContext context) : base(context)
    {
    }

    public AccessLevel? FindByValue(AccessLevels value)
        => _context.AccessLevels
            .Where(level => level.Value.Equals(value))
            .SingleOrDefault();
}

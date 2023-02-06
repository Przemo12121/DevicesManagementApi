using Database.Contexts;
using Database.Models;
using Database.Models.Enums;
using Database.Models.Interfaces;
using Database.Repositories.InnerDependencies;
using Database.Repositories.Interfaces;

namespace Database.Repositories;

public class AccessLevelsRepository : DisposableRepository<LocalAuthStorageContext>, IAccessLevelsRepository
{
    public AccessLevelsRepository(LocalAuthStorageContext context) : base(context)
    {
    }

    public AccessLevel? FindByValue(AccessLevels value)
        => _context.AccessLevels
            .Where(leve => leve.Value.Equals(value))
            .SingleOrDefault();
}

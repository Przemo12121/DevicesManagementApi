using Database.Models.Enums;
using Database.Models.Interfaces;

namespace Database.Repositories.Interfaces;

public interface IAccessLevelsRepository<T>
    where T : IAccessLevel
{
    public T? FindByValue(AccessLevels value);
}

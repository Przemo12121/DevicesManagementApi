using Database.Models;
using Database.Models.Enums;

namespace Database.Repositories.Interfaces;

public interface IAccessLevelsRepository
{
    public AccessLevel? FindByValue(AccessLevels value);
}

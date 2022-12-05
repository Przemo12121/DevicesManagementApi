using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    /// <summary>
    /// Allows for using commands table.
    /// </summary>
    public interface ICommandsRepository : IUpdatableRepository<ICommand>
    {
    }
}

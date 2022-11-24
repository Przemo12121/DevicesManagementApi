using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    /// <summary>
    /// Allows for using devices table, and associated message histories and command histories to them.
    /// </summary>
    public interface IDeviceRepository : IDisposable, IUpdatableRepository<IDevice>, ICreatableRepository<IDevice>
    {
        /// <summary>
        /// Finds device entity by input primary key.
        /// </summary>
        /// <param name="id">Requested primary key.</param>
        /// <returns>Instance of device entity.</returns>
        IDevice FindByIn(int id);

        /// <summary>
        /// Finds all devices.
        /// </summary>
        /// <returns>List of all devices.</returns>
        List<IDevice> FindAll();

        /// <summary>
        /// Finds all devices matched by a query
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        List<IDevice> FindAll(ISearchOptions<IDevice> options);
        List<IDevice> FindAllByEmployeeId(string employeeId);
        List<IDevice> FindAllByEmployeeId(string employeeId, ISearchOptions<IDevice> options);

        void GetCommands();
        void GetCommands(ISearchOptions<ICommand> options);
        void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> command);
        void DeleteCommand(IDevice device);

        void GetHistory();
        void GetHistory(ISearchOptions<ICommandHistory> options);
        void AddHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> command);
    }
}

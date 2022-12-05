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
        IDevice FindById(int id);

        /// <summary>
        /// Finds all devices.
        /// </summary>
        /// <returns>List of all devices.</returns>
        List<IDevice> FindAll();

        /// <summary>
        /// Finds all devices matched by a query options.
        /// </summary>
        /// <param name="options">Options to build the query on.</param>
        /// <returns>List of devices that matched query.</returns>
        List<IDevice> FindAll(ISearchOptions<IDevice> options);

        /// <summary>
        /// Finds all devices belonging to employee specified by id.
        /// </summary>
        /// <param name="employeeId">Unique Id of employee.</param>
        /// <returns>List of matched devices.</returns>
        List<IDevice> FindAllByEmployeeId(string employeeId);

        /// <summary>
        /// Finds all devices belonging to employee specified by id.
        /// </summary>
        /// <param name="employeeId">Unique Id of employee.</param>
        /// <param name="options">Options to build the query on.</param>
        /// <returns>List of matched devices.</returns>
        List<IDevice> FindAllByEmployeeId(string employeeId, ISearchOptions<IDevice> options);

        /// <summary>
        /// Finds all commands belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <returns>List of matched commands.</returns>
        List<ICommand> GetCommands(int deviceId);

        /// <summary>
        /// Finds all commands belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <param name="options">Options to build the query on.</param>
        /// <returns>List of matched commands.</returns>
        List<ICommand> GetCommands(int deviceId, ISearchOptions<ICommand> options);

        /// <summary>
        /// Creates new command built with the builder. New command belongs to input device.
        /// </summary>
        /// <param name="device">The device to add the command to.</param>
        /// <param name="builder">The builder to build the command with.</param>
        void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> builder);


        /// <summary>
        /// Finds all command history belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <returns>List of matched command history.</returns>
        List<ICommandHistory> GetCommandHistory(int deviceId);

        /// <summary>
        /// Finds all command history belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <param name="options">Options to build the query on.</param>
        /// <returns>List of matched command history.</returns>
        List<ICommandHistory> GetCommandHistory(int deviceId, ISearchOptions<ICommandHistory> options);
        
        /// <summary>
        /// Creates new instance of command history with given builder, and assigns it to given device.
        /// </summary>
        /// <param name="device">The device to create the command history for.</param>
        /// <param name="builder">The builder to build the command history with.</param>
        void AddCommandHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder);

        /// <summary>
        /// Finds all command history belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <returns>List of matched command history.</returns>
        List<IMessage> GetMessageHistory(int deviceId);

        /// <summary>
        /// Finds all command history belonging to device specified by the id.
        /// </summary>
        /// <param name="deviceId">Unique Id of device.</param>
        /// <param name="options">Options to build the query on.</param>
        /// <returns>List of matched command history.</returns>
        List<IMessage> GetMessageHistory(int deviceId, ISearchOptions<ICommandHistory> options);

        /// <summary>
        /// Creates new instance of message history with given builder, and assings it to given device.
        /// </summary>
        /// <param name="device">The device to create message history for.</param>
        /// <param name="builder">The builder to build the message history with.</param>
        void AddMessageHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder);
    }
}

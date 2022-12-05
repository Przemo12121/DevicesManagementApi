using Database.Models.Interfaces;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Allows for using devices table, and associated message histories and command histories to them.
/// </summary>
public interface IDeviceRepository : IDisposable, IUpdatableRepository<IDevice>, ICreatableRepository<IDevice>
{
    IDevice FindById(int id);
    List<IDevice> FindAll();
    List<IDevice> FindAll(ISearchOptions<IDevice> options);
    List<IDevice> FindAllByEmployeeId(string employeeId);
    List<IDevice> FindAllByEmployeeId(string employeeId, ISearchOptions<IDevice> options);
    List<ICommand> GetCommands(int deviceId);
    List<ICommand> GetCommands(int deviceId, ISearchOptions<ICommand> options);
    void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> builder);
    List<ICommandHistory> GetCommandHistory(int deviceId);
    List<ICommandHistory> GetCommandHistory(int deviceId, ISearchOptions<ICommandHistory> options);
    void AddCommandHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder);
    List<IMessage> GetMessageHistory(int deviceId);
    List<IMessage> GetMessageHistory(int deviceId, ISearchOptions<ICommandHistory> options);
    void AddMessageHistory(IDevice device, ICreatableModelBuilder<ICommandHistory> builder);
}

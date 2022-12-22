using Database.Models.Interfaces;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Allows for using devices table, and associated message histories and command histories to them.
/// </summary>
public interface IDeviceRepository<T, U, V, W> : IDisposable 
    where T : IDevice 
    where U : ICommand
    where V : ICommandHistory
    where W : IMessage
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void AddMessageToHistory(T device, W message);
    T? FindById(Guid id);
    List<T> FindAll<TOrderKey>(ISearchOptions<T, TOrderKey> options);
    List<T> FindAllByEmployeeId<TOrderKey>(string employeeId, ISearchOptions<T, TOrderKey> options);
    List<ICommand> GetCommands<TOrderKey>(int deviceId, ISearchOptions<ICommand, TOrderKey> options);
    void AddCommand(T device, U command);
    List<ICommandHistory> GetCommandHistory<TOrderKey>(int deviceId, ISearchOptions<ICommandHistory, TOrderKey> options);
    void AddCommandHistory(T device, V commandHistory);
    List<IMessage> GetMessageHistory<TOrderKey>(int deviceId, ISearchOptions<ICommandHistory, TOrderKey> options);
}

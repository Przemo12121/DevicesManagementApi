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
    void AddMessage(T device, W message);
    T? FindById(Guid id);
    List<T> FindAll<TOrderKey>(ISearchOptions<T, TOrderKey> options);
    List<T> FindAllByEmployeeId<TOrderKey>(string employeeId, ISearchOptions<T, TOrderKey> options);
    List<U> GetCommands<TOrderKey>(Guid deviceId, ISearchOptions<U, TOrderKey> options);
    void AddCommand(T device, U command);
    List<V> GetCommandHistories<TOrderKey>(Guid deviceId, ISearchOptions<V, TOrderKey> options);
    List<W> GetMessages<TOrderKey>(Guid deviceId, ISearchOptions<W, TOrderKey> options);
}

using Database.Models.Interfaces;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Allows for using devices table, and associated message histories and command histories to them.
/// </summary>
public interface IDeviceRepository<T> : IDisposable where T : IDevice
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void AddMessageHistory(T device, ICreatableModelBuilder<ICommandHistory> builder);
    T? FindById(Guid id);
    List<T> FindAll<U>(ISearchOptions<T, U> options);
    List<T> FindAllByEmployeeId<U>(string employeeId, ISearchOptions<T, U> options);
    List<ICommand> GetCommands<U>(int deviceId, ISearchOptions<ICommand, U> options);
    void AddCommand(T device, ICreatableModelBuilder<ICommand> builder);
    List<ICommandHistory> GetCommandHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options);
    void AddCommandHistory(T device, ICreatableModelBuilder<ICommandHistory> builder);
    List<IMessage> GetMessageHistory<U>(int deviceId, ISearchOptions<ICommandHistory, U> options);
}

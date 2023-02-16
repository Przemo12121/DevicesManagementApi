using Database.Models;

namespace Database.Repositories.Interfaces;

public interface IDevicesRepository : IDisposable, ITransactionableRepository, IResourceAuthorizableRepository<Device>
{
    void Add(Device entity);
    void Update(Device entity);
    void Delete(Device entity);
    void AddMessage(Device device, Message message);
    Device? FindById(Guid id);
    List<Device> FindAll<TOrderKey>(ISearchOptions<Device, TOrderKey> options);
    List<Device> FindAllByEmployeeId<TOrderKey>(string employeeId, ISearchOptions<Device, TOrderKey> options);
    List<Command> GetCommands<TOrderKey>(Guid deviceId, ISearchOptions<Command, TOrderKey> options);
    void AddCommand(Device device, Command command);
    List<CommandHistory> GetCommandHistories<TOrderKey>(Guid deviceId, ISearchOptions<CommandHistory, TOrderKey> options);
    List<Message> GetMessages<TOrderKey>(Guid deviceId, ISearchOptions<Message, TOrderKey> options);
    int Count(Func<Device, bool> predicate);
    int Count();
}

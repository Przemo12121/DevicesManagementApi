using Database.Models;
using System.Linq.Expressions;

namespace Database.Repositories.Interfaces;

public interface IDevicesRepository : IDisposable, ITransactionableRepository, IResourceAuthorizableRepository<Device>
{
    void Add(Device entity);
    void Update(Device entity);
    void Delete(Device entity);
    void AddMessage(Device device, Message message);
    void AddCommand(Device device, Command command);

    Task<List<Device>> FindAllAsync<TOrderKey>(ISearchOptions<Device, TOrderKey> options);
    Task<List<Device>> FindAllByEmployeeIdAsync<TOrderKey>(string employeeId, ISearchOptions<Device, TOrderKey> options);
    Task<List<Command>> GetCommandsAsync<TOrderKey>(Guid deviceId, ISearchOptions<Command, TOrderKey> options);
    Task<List<CommandHistory>> GetCommandHistoriesAsync<TOrderKey>(Guid deviceId, ISearchOptions<CommandHistory, TOrderKey> options);
    Task<List<Message>> GetMessagesAsync<TOrderKey>(Guid deviceId, ISearchOptions<Message, TOrderKey> options);
    Task<int> CountAsync(Expression<Func<Device, bool>> predicate);
    Task<int> CountAsync();
    Task<int> CountCommandsAsync(Guid deviceId);
}

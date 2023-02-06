using Database.Models;
namespace Database.Repositories.Interfaces;

public interface ICommandsRepository : IResourceAuthorizableRepository<Command>, ITransactionableRepository
{
    void Update(Command entity);
    void Delete(Command entity);
    void AddCommandHistory(Command command, CommandHistory commandHistory);
}

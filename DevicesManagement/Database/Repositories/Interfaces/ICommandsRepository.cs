using Database.Models.Interfaces;
using Database.Repositories.InnerDependencies;

namespace Database.Repositories.Interfaces;

public interface ICommandsRepository<T, U> : IResourceAuthorizableRepository<T>
    where T : ICommand
    where U : ICommandHistory
{
    void Update(T entity);
    void Delete(T entity);
    void AddCommandHistory(T command, U commandHistory);
}

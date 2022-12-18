
using Database.Models.Interfaces;

namespace Database.Repositories.Interfaces;

public interface ICommandsRepository<T> where T : ICommand
{
    void Update(T entity);
    void Delete(T entity);
}

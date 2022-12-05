using Database.Models.Base;
using Database.Repositories.Builders;

namespace Database.Repositories.Interfaces;

/// <summary>
/// Allows for creating and deleting entities in the database.
/// </summary>
/// <typeparam name="T">Cratable entity type.</typeparam>
public interface ICreatableRepository<T> where T : ICreatableModel
{
    void Add(ICreatableModelBuilder<T> builder);

    void Delete(T entity);
}

/// <summary>
/// Allows for performing CRUD operations with entities in the database.
/// </summary>
/// <typeparam name="T">Entity on which all CRUD operations can be performed.</typeparam>
public interface IUpdatableRepository<T> where T : IUpdatableModel
{
    void Update(IUpdatableModelBuilder<T> builder);
}

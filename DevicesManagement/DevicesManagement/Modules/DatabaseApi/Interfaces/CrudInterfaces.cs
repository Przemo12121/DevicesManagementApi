using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    /// <summary>
    /// Allows for creating and deleting entities in the database.
    /// </summary>
    /// <typeparam name="T">Cratable entity type.</typeparam>
    public interface ICreatableRepository<T> where T : ICreatableModel
    {
        /// <summary>
        /// Inserts new entity, built with the builder, to the database.
        /// </summary>
        /// <param name="builder">The builder used for constructing the entity.</param>
        void Add(ICreatableModelBuilder<T> builder);

        /// <summary>
        /// Deletes requested entity from the database.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        void Delete(T entity);
    }

    /// <summary>
    /// Allows for performing CRUD operations with entities in the database.
    /// </summary>
    /// <typeparam name="T">Entity on which all CRUD operations can be performed.</typeparam>
    public interface IUpdatableRepository<T> where T : IUpdatableModel
    {
        /// <summary>
        /// Updates entity in the database, based on provided builder.
        /// </summary>
        /// <param name="builder">The builder used for creating update query.</param>
        void Update(IUpdatableModelBuilder<T> builder);
    }
}

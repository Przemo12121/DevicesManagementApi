using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi.Builders;

/// <summary>
/// Builder creating editable database entities.
/// </summary>
/// <typeparam name="T">Updatable database entity.</typeparam>
public interface IUpdatableModelBuilder<T> where T : IUpdatableModel
{
    /// <summary>
    /// Builds requested entity.
    /// </summary>
    /// <returns>Updatable database entity.</returns>
    public T Build(T updatedEntity);
}

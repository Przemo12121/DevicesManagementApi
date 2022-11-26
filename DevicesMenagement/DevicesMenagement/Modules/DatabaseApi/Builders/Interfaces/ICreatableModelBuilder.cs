using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi.Builders;

/// <summary>
/// Builder creating insertable database entities.
/// </summary>
/// <typeparam name="T">Creatable database entity.</typeparam>
public interface ICreatableModelBuilder<T> where T : ICreatableModel
{
    /// <summary>
    /// Builds requested entity.
    /// </summary>
    /// <returns>Creatable database entity.</returns>
    public T Build();
}

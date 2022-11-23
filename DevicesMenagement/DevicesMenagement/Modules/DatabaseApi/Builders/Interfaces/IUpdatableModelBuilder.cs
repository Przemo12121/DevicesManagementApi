using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi.Builders;

public interface IUpdatableModelBuilder<T> where T : IUpdatableModel
{
    public T Build(T updatedEntity);
}

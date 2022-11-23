using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi.Builders;

public interface ICreatableModelBuilder<T> where T : ICreatableModel
{
    public T Build();
}

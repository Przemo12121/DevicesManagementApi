using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ICreatableRepository<T> where T : ICreatableModel
    {
        void Add(ICreatableModelBuilder<T> builder);
        void Delete(T entity);
    }

    public interface IUpdatableRepository<T> where T : IUpdatableModel
    {
        void Update(IUpdatableModelBuilder<T> builder);
    }
}

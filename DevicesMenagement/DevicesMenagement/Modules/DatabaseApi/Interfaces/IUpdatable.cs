using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface IUpdatable<T> where T : UpdatableModel
    {
        public T Update(T entity, T newData); // newData shall have all nullable fields of T (new type)
    }
}

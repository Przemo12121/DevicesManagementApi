using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface IInsertable<T> where T : CreatableModel
    {
        public T Insert(T entity);
    }
}

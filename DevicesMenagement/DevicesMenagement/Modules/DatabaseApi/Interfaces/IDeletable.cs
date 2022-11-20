using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface IDeletable<T> where T : DatabaseModel
    {
        public void Delete(T entity);
    }
}

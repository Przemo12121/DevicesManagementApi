using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ISelectable<T> where T : DatabaseModel
    {
        public List<T> GetAll(ISelectOptions<T> options);
        public T GetById(int id);
    }
}

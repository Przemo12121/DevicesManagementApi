using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ISelectOptions<T> where T : DatabaseModel
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Order { get; set; }
        public Func<bool, T> Where { get; set; }
    }
}

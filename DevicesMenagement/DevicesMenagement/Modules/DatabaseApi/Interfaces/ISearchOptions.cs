using DevicesMenagement.Database.Models;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface ISearchOptions<T> where T : IDatabaseModel
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public Func<bool, T> Order { get; set; }
    }
}

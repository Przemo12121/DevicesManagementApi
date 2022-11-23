using DevicesMenagement.Database.Models;
using DevicesMenagement.Modules.DatabaseApi.Builders;

namespace DevicesMenagement.Modules.DatabaseApi
{
    public interface IDeviceRepository : IDisposable, IUpdatableRepository<IDevice>, ICreatableRepository<IDevice>
    {
        IDevice FindByIn(int id);
        List<IDevice> FindAll();
        List<IDevice> FindAll(ISearchOptions<IDevice> options);
        List<IDevice> FindAllByEmployeeId(string employeeId);
        List<IDevice> FindAllByEmployeeId(string employeeId, ISearchOptions<IDevice> options);

        void AddCommand(IDevice device, ICreatableModelBuilder<ICommand> command);
        void AddHistory(IDevice device, ICreatableModelBuilder<IDeviceHistory> command);

        void DeleteCommand(IDevice device);
        void DeleteHistory(IDevice device);
    }
}

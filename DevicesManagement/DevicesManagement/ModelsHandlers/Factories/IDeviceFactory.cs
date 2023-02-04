using Database.Models;
using Database.Models.Interfaces;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories;

public interface IDeviceFactory<T>
    where T : IDevice
{
    T From(RegisterDeviceRequest request, string employeeId);
}

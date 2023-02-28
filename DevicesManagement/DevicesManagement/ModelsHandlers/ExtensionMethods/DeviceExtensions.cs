using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.ExtensionMethods;

public static class DeviceExtensions
{
    public static void UpdateWith(this Device device, UpdateDeviceRequest request)
    {
        if (request.Name is not null)
        {
            device.Name = request.Name;
        }
        if (request.Address is not null)
        {
            device.Address = request.Address;
        }

        device.UpdatedDate = DateTime.UtcNow;
    }
}

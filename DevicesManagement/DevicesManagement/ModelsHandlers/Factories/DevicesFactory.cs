using Database.Models;
using DevicesManagement.DataTransferObjects.Requests;

namespace DevicesManagement.ModelsHandlers.Factories;

public class DevicesFactory : IDeviceFactory<Device>
{
    public Device From(RegisterDeviceRequest request, string employeeId)
        => new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Name = request.Name,
            Address = request.Address,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Commands = new(),
            Messages = new()
        };
}

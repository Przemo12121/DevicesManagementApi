using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, DeviceDto>
{
    public Task<DeviceDto> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

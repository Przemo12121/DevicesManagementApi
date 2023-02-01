using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class RegisterDeviceCommandHandler : IRequestHandler<RegisterDeviceCommand, DeviceDto>
{
    public Task<DeviceDto> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

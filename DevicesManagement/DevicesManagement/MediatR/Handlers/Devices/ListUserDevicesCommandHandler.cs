using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListUserDevicesCommandHandler : IRequestHandler<ListUserDevicesCommand, List<DeviceDto>>
{
    public Task<List<DeviceDto>> Handle(ListUserDevicesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

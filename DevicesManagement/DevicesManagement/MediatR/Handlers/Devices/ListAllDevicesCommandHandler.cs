using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListAllDevicesCommandHandler : IRequestHandler<GetAllDevicesQuery, List<DeviceDto>>
{
    public Task<List<DeviceDto>> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

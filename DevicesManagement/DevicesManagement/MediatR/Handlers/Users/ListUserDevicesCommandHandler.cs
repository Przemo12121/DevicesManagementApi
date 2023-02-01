using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Users;

public class ListUserDevicesCommandHandler : IRequestHandler<GetUserDevicesQuery, List<DeviceDto>>
{
    public Task<List<DeviceDto>> Handle(GetUserDevicesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

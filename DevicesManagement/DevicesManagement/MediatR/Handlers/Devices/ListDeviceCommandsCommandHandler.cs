using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListDeviceCommandsCommandHandler : IRequestHandler<GetDeviceCommandsQuery, List<CommandDto>>
{
    public Task<List<CommandDto>> Handle(GetDeviceCommandsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListDeviceCommandsCommandHandler : IRequestHandler<ListDeviceCommandsCommand, List<CommandDto>>
{
    public Task<List<CommandDto>> Handle(ListDeviceCommandsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

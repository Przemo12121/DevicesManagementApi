using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListDeviceCommandsCommandHandler : IRequestHandler<GetDeviceCommandsQuery, IActionResult>
{
    public Task<IActionResult> Handle(GetDeviceCommandsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

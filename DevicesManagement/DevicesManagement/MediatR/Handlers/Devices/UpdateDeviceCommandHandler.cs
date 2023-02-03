using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, IActionResult>
{
    public Task<IActionResult> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

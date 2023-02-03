using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class RegisterDeviceCommandHandler : IRequestHandler<RegisterDeviceCommand, IActionResult>
{
    public Task<IActionResult> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

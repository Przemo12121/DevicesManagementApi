using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class ListUserDevicesCommandHandler : IRequestHandler<GetUserDevicesQuery, IActionResult>
{
    public Task<IActionResult> Handle(GetUserDevicesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

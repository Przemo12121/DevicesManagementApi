using Database.Repositories;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListAllDevicesCommandHandler : IRequestHandler<GetAllDevicesQuery, IActionResult>
{
    private readonly DevicesRepository _devicesRepository;

    public ListAllDevicesCommandHandler(DevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }

    public Task<IActionResult> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        //var options = new Sear

        throw new NotImplementedException();
    }
}

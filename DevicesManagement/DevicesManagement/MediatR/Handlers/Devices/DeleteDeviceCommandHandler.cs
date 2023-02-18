using Database.Repositories.Interfaces;
using DevicesManagement.MediatR.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;

    public DeleteDeviceCommandHandler(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }

    public async Task<IActionResult> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        _devicesRepository.Delete(request.Resource!);
        await _devicesRepository.SaveAsync();

        return new OkResult();
    }
}

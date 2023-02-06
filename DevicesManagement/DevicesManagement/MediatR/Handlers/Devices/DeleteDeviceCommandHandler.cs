using Database.Repositories.Interfaces;
using DevicesManagement.MediatR.Commands.Devices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, IActionResult>
{
    private readonly IDevicesRepository _deviceRepository;

    public DeleteDeviceCommandHandler(IDevicesRepository devicesRepository)
    {
        _deviceRepository = devicesRepository;
    }

    public Task<IActionResult> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        _deviceRepository.Delete(request.Resource!);
        _deviceRepository.SaveChanges();

        return Task.FromResult<IActionResult>(new OkResult());
    }
}

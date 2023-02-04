using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.ExtensionMethods;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;

    public UpdateDeviceCommandHandler(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }

    public Task<IActionResult> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        request.Resource.UpdateWith(request.Request);

        _devicesRepository.Update(request.Resource);
        _devicesRepository.SaveChanges();

        var result = new OkObjectResult(request.Resource.Adapt<DeviceDto>());
        return Task.FromResult<IActionResult>(result);
    }
}

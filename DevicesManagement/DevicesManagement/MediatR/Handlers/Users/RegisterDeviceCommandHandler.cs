using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories;
using Mapster;
using MediatR;
using Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class RegisterDeviceCommandHandler : IRequestHandler<RegisterDeviceCommand, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly IDeviceFactory<Device> _deviceFactory;

    public RegisterDeviceCommandHandler(IDeviceFactory<Device> deviceFactory, IDevicesRepository devicesRepository)
    {
        _deviceFactory = deviceFactory;
        _devicesRepository = devicesRepository;
    }

    public Task<IActionResult> Handle(RegisterDeviceCommand request, CancellationToken cancellationToken)
    {
        var newDevice = _deviceFactory.From(request.Request, request.Resource.EmployeeId);

        _devicesRepository.Add(newDevice);
        _devicesRepository.SaveChanges();

        var result = new OkObjectResult(newDevice.Adapt<DeviceDto>());
        return Task.FromResult<IActionResult>(result);
    }
}

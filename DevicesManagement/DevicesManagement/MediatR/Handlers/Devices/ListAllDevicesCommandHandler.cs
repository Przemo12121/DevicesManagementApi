using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class ListAllDevicesCommandHandler : IRequestHandler<GetAllDevicesQuery, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly ISearchOptionsFactory<Device, string> _searchOptionsFactory;

    public ListAllDevicesCommandHandler(IDevicesRepository devicesRepository, ISearchOptionsFactory<Device, string> searchOptionsFactory)
    {
        _devicesRepository = devicesRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public Task<IActionResult> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var devices = _devicesRepository.FindAll(options);

        var result = new OkObjectResult(devices.Adapt<List<DeviceDto>>());
        return Task.FromResult<IActionResult>(result);
    }
}

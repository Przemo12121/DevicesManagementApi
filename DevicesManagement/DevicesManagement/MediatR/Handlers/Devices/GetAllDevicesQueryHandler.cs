using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class GetAllDevicesQueryHandler : IRequestHandler<GetAllDevicesQuery, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly ISearchOptionsFactory<Device, string> _searchOptionsFactory;

    public GetAllDevicesQueryHandler(IDevicesRepository devicesRepository, ISearchOptionsFactory<Device, string> searchOptionsFactory)
    {
        _devicesRepository = devicesRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public Task<IActionResult> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var devices = _devicesRepository.FindAll(options);
        var totalCount = _devicesRepository.Count();

        var result = new OkObjectResult(
            new PaginationResponseDto<DeviceDto>(totalCount, devices.Adapt<List<DeviceDto>>())
        );
        return Task.FromResult<IActionResult>(result);
    }
}

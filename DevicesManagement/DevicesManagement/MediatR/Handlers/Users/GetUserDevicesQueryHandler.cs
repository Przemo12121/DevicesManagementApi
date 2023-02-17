using Database.Models;
using Database.Repositories.Interfaces;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetUserDevicesQueryHandler : IRequestHandler<GetUserDevicesQuery, IActionResult>
{
    private readonly IDevicesRepository _devicesRepository;
    private readonly ISearchOptionsFactory<Device, string> _searchOptionsFactory;

    public GetUserDevicesQueryHandler(IDevicesRepository devicesRepository, ISearchOptionsFactory<Device, string> searchOptionsFactory)
    {
        _devicesRepository = devicesRepository;
        _searchOptionsFactory = searchOptionsFactory;
    }
    public Task<IActionResult> Handle(GetUserDevicesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var devices = _devicesRepository.FindAllByEmployeeId(request.Resource.EmployeeId, options);
        var totalCount = _devicesRepository.Count(device => device.EmployeeId.Equals(request.Resource.EmployeeId));

        var result = new OkObjectResult(
            new PaginationResponseDto<DeviceDto>(totalCount, devices.Adapt<List<DeviceDto>>())
        );
        return Task.FromResult<IActionResult>(result);
    }
}

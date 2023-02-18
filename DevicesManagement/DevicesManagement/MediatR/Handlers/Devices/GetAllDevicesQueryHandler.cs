using Database.Models;
using Database.Repositories.Interfaces;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class GetAllDevicesQueryHandler : IRequestHandler<GetAllDevicesQuery, IActionResult>
{
    private readonly IDevicesManagementParallelRepositoriesFactory _devicesManagementParallelRepositoriesFactory;
    private readonly ISearchOptionsFactory<Device, string> _searchOptionsFactory;

    public GetAllDevicesQueryHandler(IDevicesManagementParallelRepositoriesFactory devicesManagementParallelRepositoriesFactory, ISearchOptionsFactory<Device, string> searchOptionsFactory)
    {
        _devicesManagementParallelRepositoriesFactory = devicesManagementParallelRepositoriesFactory;
        _searchOptionsFactory = searchOptionsFactory;
    }

    public Task<IActionResult> Handle(GetAllDevicesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var devices = _devicesManagementParallelRepositoriesFactory.CreateDevicesRepository()
            .FindAllAsync(options);
        var totalCount = _devicesManagementParallelRepositoriesFactory.CreateDevicesRepository()
            .CountAsync();

        Task.WaitAll(new Task[] { devices, totalCount }, cancellationToken);
        var result = new OkObjectResult(
            new PaginationResponseDto<DeviceDto>(
                totalCount.Result, 
                devices.Result.Adapt<List<DeviceDto>>()
            )
        );

        return Task.FromResult<IActionResult>(result);
    }
}

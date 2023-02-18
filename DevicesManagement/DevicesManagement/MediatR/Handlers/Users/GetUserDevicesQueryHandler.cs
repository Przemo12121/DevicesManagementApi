using Database.Models;
using Database.Repositories.Interfaces;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Users;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Users;

public class GetUserDevicesQueryHandler : IRequestHandler<GetUserDevicesQuery, IActionResult>
{
    private readonly IDevicesManagementParallelRepositoriesFactory _parrallelRepositoriesFactory;
    private readonly ISearchOptionsFactory<Device, string> _searchOptionsFactory;

    public GetUserDevicesQueryHandler(IDevicesManagementParallelRepositoriesFactory parallelRepositoriesFactory, ISearchOptionsFactory<Device, string> searchOptionsFactory)
    {
        _parrallelRepositoriesFactory = parallelRepositoriesFactory;
        _searchOptionsFactory = searchOptionsFactory;
    }
    public async Task<IActionResult> Handle(GetUserDevicesQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.From(request.Request);

        var devices = _parrallelRepositoriesFactory.CreateDevicesRepository()
            .FindAllByEmployeeIdAsync(request.Resource.EmployeeId, options);
        var totalCount = _parrallelRepositoriesFactory.CreateDevicesRepository()
            .CountAsync(device => device.EmployeeId.Equals(request.Resource.EmployeeId));

        await devices;
        await totalCount;

        Task.WaitAll(new Task[] { devices, totalCount }, cancellationToken);
        var result =  new OkObjectResult(
            new PaginationResponseDto<DeviceDto>(
                totalCount.Result, 
                devices.Result.Adapt<List<DeviceDto>>()
            )
        );

        return result;
        //return Task.FromResult<IActionResult>(result);
    }
}

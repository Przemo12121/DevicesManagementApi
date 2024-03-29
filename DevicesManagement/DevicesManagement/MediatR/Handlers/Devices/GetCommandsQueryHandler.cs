﻿using Database.Models;
using Database.Repositories.Interfaces;
using Database.Repositories.ParallelRepositoryFactories;
using DevicesManagement.DataTransferObjects.Responses;
using DevicesManagement.MediatR.Commands.Devices;
using DevicesManagement.ModelsHandlers.Factories.SearchOptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevicesManagement.MediatR.Handlers.Devices;

public class GetCommandsQueryHandler : IRequestHandler<GetCommandsQuery, IActionResult>
{
    private readonly IDevicesManagementParallelRepositoriesFactory _parallelRepositoriesFactory;
    private readonly ISearchOptionsFactory<Command, string> _searchOptionsFactory;

    public GetCommandsQueryHandler(
        IDevicesManagementParallelRepositoriesFactory parallelRepositoriesFactory, 
        ISearchOptionsFactory<Command, string> searchOptionsFactory)
    {
        _parallelRepositoriesFactory = parallelRepositoriesFactory;
        _searchOptionsFactory = searchOptionsFactory;
    }
    public async Task<IActionResult> Handle(GetCommandsQuery request, CancellationToken cancellationToken)
    {
        var options = _searchOptionsFactory.CreateFromRequest(request.Request);

        var commands = _parallelRepositoriesFactory.CreateDevicesRepository()
            .GetCommandsAsync(request.Resource.Id, options);
        var totalCount = _parallelRepositoriesFactory.CreateDevicesRepository()
            .CountCommandsAsync(request.Resource.Id);

        await Task.WhenAll(new Task[] { commands, totalCount });
        var result = new OkObjectResult(
            new PaginationResponseDto<CommandDto>(
                totalCount.Result, 
                commands.Result.Adapt<List<CommandDto>>()
            )
        );

        return result;
    }
}
